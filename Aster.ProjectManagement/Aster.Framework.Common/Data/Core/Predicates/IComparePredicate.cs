using Aster.Framework.Common.Data.Core.Enums;

namespace Aster.Framework.Common.Data.Core.Predicates
{
    public interface IComparePredicate : IBasePredicate
    {
        Operator Operator { get; set; }
        bool Not { get; set; }
    }
}