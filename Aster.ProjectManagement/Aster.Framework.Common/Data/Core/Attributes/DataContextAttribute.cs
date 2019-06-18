using System;

namespace Aster.Framework.Common.Data.Core.Attributes
{
    public class DataContextAttribute : Attribute
    {
        public string ConnectionStringName { get; private set; }

        public DataContextAttribute(string connectionStringName) { ConnectionStringName = connectionStringName; }
    }
}
