﻿using System;
using System.Collections.Generic;
using Aster.Framework.Common.Data.Core.Configuration;
using Aster.Framework.Common.Data.Core.Mapper;
using Aster.Framework.Common.Data.Core.Predicates;
using Aster.Framework.Common.Data.Core.Sql;

namespace Aster.Framework.Common.Data.Predicates
{
    public class ExistsPredicate<TSub> : IExistsPredicate where TSub : class
    {
        public IPredicate Predicate { get; set; }
        public bool Not { get; set; }

        public string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            IClassMapper mapSub = GetClassMapper(typeof(TSub), sqlGenerator.Configuration);
            string sql = string.Format("({0}EXISTS (SELECT 1 FROM {1} WHERE {2}))",
                Not ? "NOT " : string.Empty,
                sqlGenerator.GetTableName(mapSub),
                Predicate.GetSql(sqlGenerator, parameters));
            return sql;
        }

        protected virtual IClassMapper GetClassMapper(Type type, DapperConfiguration configuration)
        {
            IClassMapper map = configuration.GetMap(type);
            if (map == null)
            {
                throw new NullReferenceException(string.Format("Map was not found for {0}", type));
            }

            return map;
        }
    }
}