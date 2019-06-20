using System.Data;

namespace Aster.Framework.Common.Data.Core.Sessions
{
    public interface IDapperSession : IDbConnection
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }

        void Commit();
        void Rollback();



    }
}
