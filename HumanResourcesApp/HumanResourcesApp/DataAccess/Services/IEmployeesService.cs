using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourcesApp.Services
{
    internal interface IEmployeesService
    {
        Task<bool> AddEmployeeIfNotExists(Employee employee);

        Task<IQueryable<Employee>> GetEmployeesAsync(int departmentId, int statusId);
    }
}
