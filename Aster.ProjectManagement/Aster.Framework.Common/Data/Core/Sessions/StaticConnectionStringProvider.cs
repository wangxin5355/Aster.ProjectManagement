using Aster.Framework.Common.Data.Core.Configuration;
using System;

namespace Aster.Framework.Common.Data.Core.Sessions
{
    public class StaticConnectionStringProvider : IConnectionStringProvider
    {
        private readonly DapperConfiguration _dapperConfiguration;

        public StaticConnectionStringProvider(DapperConfiguration dapperConfiguration)
        {
            _dapperConfiguration = dapperConfiguration;
        }

        public string ConnectionString(string connectionStringName)
        {
            if (!_dapperConfiguration.AllConnectionStrings.ContainsKey(connectionStringName))
                throw new NullReferenceException(string.Format("Connection string '{0}' not found", connectionStringName));

            return _dapperConfiguration.AllConnectionStrings[connectionStringName];
        }
    }

}
