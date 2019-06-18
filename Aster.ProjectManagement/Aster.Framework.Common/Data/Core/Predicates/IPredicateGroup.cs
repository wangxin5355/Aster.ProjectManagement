using System.Collections.Generic;
using Aster.Framework.Common.Data.Core.Enums;

namespace Aster.Framework.Common.Data.Core.Predicates
{
    public interface IPredicateGroup : IPredicate
    {
        GroupOperator Operator { get; set; }
        IList<IPredicate> Predicates { get; set; }
    }
}