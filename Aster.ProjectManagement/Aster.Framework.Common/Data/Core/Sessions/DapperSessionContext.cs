﻿using Aster.Framework.Common.Data.Core.Configuration;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Aster.Framework.Common.Data.Core.Sessions
{
    public class DapperSessionContext : IDapperSessionContext
    {
        private readonly Logger logger= LogManager.GetCurrentClassLogger();
        private readonly DapperConfiguration _configuration;
        private readonly DapperSessionFactory _factory;
        private readonly Lazy<ConcurrentDictionary<string, IDapperSession>> _sessionMap;

        public DapperSessionContext( )
        {
            _factory = DapperSessionFactory.Instance;
            _configuration = DapperConfiguration.Instance;
            IsolationLevel = IsolationLevel.ReadCommitted;
            _sessionMap = new Lazy<ConcurrentDictionary<string, IDapperSession>>(() => new ConcurrentDictionary<string, IDapperSession>());
        }

        public IDapperSession GetSession<TEntity>() where TEntity : class, IEntity
        {
            return GetSession(typeof(TEntity));
        }

        public IDapperSession GetSession(Type entityType)
        {
            //获取数据库连接串名称
            var connectionStringName = _factory.GetTypeConnectionStringName(entityType);

            return GetSession(connectionStringName);
        }

        public IDapperSession GetSession(string connectionStringName)
        {
            if (!_sessionMap.Value.TryGetValue(connectionStringName, out IDapperSession session))
            {
                session = _factory.GetSession(connectionStringName);
                _sessionMap.Value.TryAdd(connectionStringName, session);
            }

            EnsureSessionOpen(session);

            return session;
        }

        public IsolationLevel IsolationLevel { get; set; }

        public void RequireNew()
        {
            RequireNew(IsolationLevel);
        }

        public void RequireNew(IsolationLevel level)
        {
            DisposeSession();
        }

        /// <summary>
        /// 回滚事务，此方法必须在一个web请求出现异常时调用
        /// </summary>
        public void Cancel()
        {
            if (_sessionMap.Value.Count == 0)
                return;

            List<string> keys = _sessionMap.Value.Keys.ToList();

            foreach (var name in keys)
            {
                _sessionMap.Value.TryGetValue(name, out IDapperSession session);

                if (session.State == ConnectionState.Open && session.Transaction != null)
                {
                    logger.Debug("Rolling back transaction");

                    session.Transaction.Rollback();
                    session.Transaction.Dispose();
                    session.Transaction = null;
                }
            }
            DisposeSession();
        }

        public void Dispose()
        {
            DisposeSession();
        }


        private void DisposeSession()
        {
            if (_sessionMap.Value.Count == 0)
                return;

            List<string> keys = _sessionMap.Value.Keys.ToList();

            foreach (var name in keys)
            {
                _sessionMap.Value.TryGetValue(name, out IDapperSession session);

                if (session.State == ConnectionState.Open)
                {
                    try
                    {
                        if (session.Transaction != null)
                        {
                            logger.Debug("Committing transaction");
                            session.Transaction.Commit();
                            session.Transaction = null;
                        }
                    }
                    finally
                    {
                        logger.Debug("Disposing opend session");
                        session.Close();
                        session.Dispose();
                        session = null;
                    }
                }
                else
                {
                    try
                    {
                        logger.Debug("Disposing not open session");
                        session.Dispose();
                        session = null;
                    }
                    catch { }
                }
            }

            _sessionMap.Value.Clear();
        }

        private void EnsureSessionOpen(IDapperSession session)
        {
            if (session.State != ConnectionState.Open)
            {
                session.Open();
            }
        }
    }
}
