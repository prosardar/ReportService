using Dapper;
using Npgsql;
using Report.Model.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Report.Data
{
    public class PgContext : IPgDbContext
    {
        protected readonly IDbConnection InnerConnection;

        public PgContext()
        {

        }

        public PgContext(string connectionString)
        {
            InnerConnection = new NpgsqlConnection(connectionString);
        }

        public virtual IDbConnection Connection
        {
            get
            {
                OpenConnection();
                return InnerConnection;
            }
        }

        public async virtual Task<T> ExecuteSingleOrDefaultAsync<T>(string query) where T : BaseDomain
        {
            return await Connection.QuerySingleOrDefaultAsync(query);
        }

        public async virtual Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query) where T : BaseDomain
        {
            return await Connection.QueryAsync<T>(query);
        }

        public void OpenConnection()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                InnerConnection.Open();
        }

        public void Dispose()
        {
            if (InnerConnection != null && InnerConnection.State != ConnectionState.Closed)
                InnerConnection.Close();
        }
    }
}
