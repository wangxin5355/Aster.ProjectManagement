using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.Framework.Common.Utils
{
    public static class ConnectionStringUtil
    {
        public static string GetConnectionString(string name)
        {
            if (ConfigurationManager.ConnectionStrings[name] == null)
            {
                return null;
            }
            var connStr = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            if (!string.IsNullOrWhiteSpace(connStr))
            {
                return connStr;
            }
            else
            {
                throw new Exception("无效连接字符串："+ name);
            }
        }
    }
}
