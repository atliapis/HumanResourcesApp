using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess.Services
{
    public interface IDepartmentsService
    {
        Task<bool> AddDepartmentIfNotExists(Department department);

        Task<List<Department>> GetAllDepartmentsAsync();
    }
}
