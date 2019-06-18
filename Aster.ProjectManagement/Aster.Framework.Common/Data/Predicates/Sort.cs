using Aster.Framework.Common.Data.Core.Predicates;

namespace Aster.Framework.Common.Data.Predicates
{
    public class Sort : ISort
    {
        public string PropertyName { get; set; }
        public bool Ascending { get; set; }
    }
}