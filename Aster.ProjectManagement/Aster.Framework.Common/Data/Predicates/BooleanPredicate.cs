using System;
using System.Collections.Generic;
using Aster.Framework.Common.Data.Core.Predicates;
using Aster.Framework.Common.Data.Core.Sql;

namespace Aster.Framework.Common.Data.Predicates
{
    public class BooleanPredicate : ComparePredicate, IFieldPredicate
    {
        public object Value { get; set; }


        public override string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters)
        {
            if (bool.TryParse(Convert.ToString(Value), out bool b) && !b)
            {
                return " 1=0 ";
            }
            return "";
        }
    }
}
