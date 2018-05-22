using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Report.Model.Domain;

namespace Report.Data
{
    public interface IDbContext : IDisposable
    {
        Task<T> ExecuteSingleOrDefaultAsync<T>(string query) where T : BaseDomain;

        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query) where T : BaseDomain;
    }
}