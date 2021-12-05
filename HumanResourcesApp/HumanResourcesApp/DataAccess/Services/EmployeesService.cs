using HumanResourcesApp.DataAccess;
using System.Threading.Tasks;

namespace HumanResourcesApp.Services
{
    public class EmployeesService: IEmployeesService
    {
        private DatabaseAccessManager _dbManager;

        public EmployeesService(HumanResourcesContext dbContext)
        {
            _dbManager = new DatabaseAccessManager(dbContext);
        }
        public async Task<bool> AddEmployeeIfNotExists(Employee employee)
        {
            return await _dbManager.AddEmployeeIfNotExists(employee);
        }
    }
}