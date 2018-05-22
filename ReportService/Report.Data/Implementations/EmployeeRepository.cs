using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Report.Data;
using Report.Model.Domain;

namespace Report.BL
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private IDbContext _dbContext;

        public EmployeeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            string query = $"select name, inn, departmentid from emps";
            return await _dbContext.ExecuteQueryAsync<Employee>(query);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            string query = $"select name, inn, departmentid from emps where id = {id}";
            return await _dbContext.ExecuteSingleOrDefaultAsync<Employee>(query);
        }

        /// <summary>
        /// По хорошему этот метод тут лишний, потому что он слишком специфичен по отношению к репозиторию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> FindAllByDepartmentAsync(int departmentId)
        {
            string query = $"select name, inn, departmentid from emps where departmentid = {departmentId}";
            return await _dbContext.ExecuteQueryAsync<Employee>(query);
        }

        #region Find methods with predicate
        public Task<IEnumerable<Employee>> FindAllAsync(Expression<Func<Employee, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}