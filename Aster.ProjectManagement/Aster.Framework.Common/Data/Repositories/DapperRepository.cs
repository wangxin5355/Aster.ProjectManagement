using Dapper;
using Aster.Framework.Common.Data.Builder;
using Aster.Framework.Common.Data.Core;
using Aster.Framework.Common.Data.Core.Builder;
using Aster.Framework.Common.Data.Core.Implementor;
using Aster.Framework.Common.Data.Core.Repositories;
using Aster.Framework.Common.Data.Core.Sessions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Aster.Framework.Common.Data.Implementor;
using Aster.Framework.Common.Data.Sql;
using Aster.Framework.Common.Data.Core.Configuration;

namespace Aster.Framework.Common.Data.Repositories
{
    public partial class DapperRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly IDapperImplementor _dapperImplementor;
        protected readonly IDapperSessionContext _sessionContext;
        protected readonly IDapperSession session;

        public DapperRepository(IDapperSessionContext sessionContext, IDapperImplementor dapperImplementor)
        {
            _dapperImplementor = dapperImplementor;
            _sessionContext = sessionContext;
        }

        public DapperRepository()
        {
            _dapperImplementor =new DapperImplementor(new SqlGeneratorImpl(DapperConfiguration.Instance));
            _sessionContext =new DapperSessionContext();
            session = _sessionContext.GetSession<T>();
        }

        public void Commit()
        {
            session.Commit();
        }

        public void Rollback()
        {
            session.Rollback();
        }

        public virtual T Get(int id)
        {
            return _dapperImplementor.Get<T>(session, id, session.Transaction, null);
        }

        public virtual T Get(long id)
        {
            return _dapperImplementor.Get<T>(session, id, session.Transaction, null);
        }

        public virtual T Get(Guid id)
        {
            return _dapperImplementor.Get<T>(session, id, session.Transaction, null);
        }

        public virtual dynamic Insert(T item)
        {
            return _dapperImplementor.Insert(session, item, session.Transaction, null);
        }

        public virtual void Insert(IEnumerable<T> items)
        {
            _dapperImplementor.Insert(session, items, session.Transaction, null);
        }

        public virtual bool Update(T item)
        {
            return _dapperImplementor.Update(session, item, session.Transaction, null);
        }

        public virtual bool Delete(T item)
        {
            return _dapperImplementor.Delete(session, item, session.Transaction, null);
        }

        public virtual IList<T> GetList()
        {
            return _dapperImplementor.GetList<T>(session, null, null, session.Transaction, null, false, null, false).ToList();
        }

        public virtual IEntityBuilder<T> Query(Expression<Func<T, bool>> predicate)
        {
            return new EntityBuilder<T>(session, predicate, _dapperImplementor);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return _dapperImplementor.Count<T>(session, QueryBuilder<T>.FromExpression(predicate), session.Transaction, null);
        }

        public virtual bool Delete(Expression<Func<T, bool>> predicate = null)
        {
            return _dapperImplementor.Delete<T>(session, QueryBuilder<T>.FromExpression(predicate), session.Transaction, null);
        }

        public virtual IEnumerable<T> Query(string sql, object param = null, int? timeout = null)
        {
            return session.Query<T>(sql, param, session.Transaction, true, timeout);
        }

        public virtual IEnumerable<dynamic> QueryDynamic(string sql, object param = null, int? timeout = null)
        {
            return session.Query<dynamic>(sql, param, session.Transaction, true, timeout);
        }

        public object QueryScalar(string sql, object param = null, int? timeout = null)
        {
            return session.ExecuteScalar(sql, param, session.Transaction, timeout);
        }

        public async Task<int> Execute(string sql, object param = null, int? timeout = null, CommandType? commandType = null)
        {

            return await session.ExecuteAsync(sql, param, session.Transaction, timeout, commandType);
        }
    }
}
