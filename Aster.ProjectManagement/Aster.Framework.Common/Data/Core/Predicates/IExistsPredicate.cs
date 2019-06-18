namespace Aster.Framework.Common.Data.Core.Predicates
{
    public interface IExistsPredicate : IPredicate
    {
        IPredicate Predicate { get; set; }
        bool Not { get; set; }
    }
}