using HumanResourcesApp.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourcesApp.Services
{
    public class EmployeesService: IEmployeesService
    {
        private readonly DatabaseAccessManager _dbManager;

        public EmployeesService(HumanResourcesContext dbContext)
        {
            _dbManager = new DatabaseAccessManager(dbContext);
        }

        public async Task<bool> AddEmployeeIfNotExists(Employee employee)
        {
            return await _dbManager.AddEmployeeIfNotExists(employee);
        }

        public async Task<IQueryable<Employee>> GetEmployeesAsync(int departmentId, int statusId)
        {
            return await _dbManager.GetEmployeesAsync(departmentId, statusId);
        }
    }
}