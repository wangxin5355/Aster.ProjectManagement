using System.Collections.Generic;
using Aster.Framework.Common.Data.Core.Sql;

namespace Aster.Framework.Common.Data.Core.Predicates
{
    public interface IPredicate
    {
        string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters);
    }
}