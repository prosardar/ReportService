using System.Data;

namespace Report.Data
{
    public interface IPgDbContext : IDbContext
    {       
        IDbConnection Connection { get; }

        void OpenConnection();
    }
}