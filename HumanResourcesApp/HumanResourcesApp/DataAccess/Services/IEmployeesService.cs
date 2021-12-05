using System.Threading.Tasks;

namespace HumanResourcesApp.Services
{
    internal interface IEmployeesService
    {
        Task<bool> AddEmployeeIfNotExists(Employee employee);
    }
}
