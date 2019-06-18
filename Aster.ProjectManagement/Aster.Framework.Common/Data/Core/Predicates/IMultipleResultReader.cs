using System.Collections.Generic;

namespace Aster.Framework.Common.Data.Core.Predicates
{
    public interface IMultipleResultReader
    {
        IEnumerable<T> Read<T>();
    }
}