using System;
using System.Collections.Generic;
using Aster.Framework.Common.Data.Core.Predicates;

namespace Aster.Framework.Common.Data.Predicates
{
    public class GetMultiplePredicateItem : IGetMultiplePredicateItem
    {
        public object Value { get; set; }
        public Type Type { get; set; }
        public IList<ISort> Sort { get; set; }
    }
}