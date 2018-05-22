using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Report.Model.Domain;

namespace Report.Data
{
    public class DepartmentRepository : IRepository<Department>
    {
        private IDbContext _dbContext;

        public DepartmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }        

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _dbContext.ExecuteQueryAsync<Department>($"select name, active from deps");
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _dbContext.ExecuteSingleOrDefaultAsync<Department>($"select name, active from deps where id = {id}");
        }

        public async Task<IEnumerable<Department>> GetActiveAsync()
        {
            return await _dbContext.ExecuteQueryAsync<Department>($"select name, active from deps where active = 1");
        }

        #region Find methods with predicate
        public Task<IEnumerable<Department>> FindAllAsync(Expression<Func<Department, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Department> FindAsync(Expression<Func<Department, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}