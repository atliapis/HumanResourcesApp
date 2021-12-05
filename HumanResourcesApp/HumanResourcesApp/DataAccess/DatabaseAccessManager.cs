using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess
{
    public class DatabaseAccessManager
    {
        private readonly HumanResourcesContext _dbContext;

        public DatabaseAccessManager(HumanResourcesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddDepartmentIfNotExists(Department department)
        {
            if (await _dbContext.Departments.FindAsync(department.Id) == null)
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            var departments = _dbContext.Departments;

            return await departments.ToListAsync();
        }

        public async Task<bool> AddStatusIfNotExists(Status status)
        {
            if (await _dbContext.Statuses.FindAsync(status.Id) == null)
            {
                _dbContext.Statuses.Add(status);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Status>> GetAllStatusesAsync()
        {
            var statuses = _dbContext.Statuses;

            return await statuses.ToListAsync();
        }

        public async Task<bool> AddEmployeeIfNotExists(Employee employee)
        {
            if (await _dbContext.Employees.FindAsync(employee.EmployeeNumber) == null)
            {
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}