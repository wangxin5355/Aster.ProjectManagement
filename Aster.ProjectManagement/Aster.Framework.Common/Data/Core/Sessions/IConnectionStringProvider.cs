﻿namespace Aster.Framework.Common.Data.Core.Sessions
{
    public interface IConnectionStringProvider
    {
        string ConnectionString(string connectionStringName);
    }
}
