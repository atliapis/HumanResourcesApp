using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess.Services
{
    public class DepartmentsService: IDepartmentsService
    {
        private readonly DatabaseAccessManager _dbManager;

        public DepartmentsService(HumanResourcesContext dbcontext)
        {
            _dbManager = new DatabaseAccessManager(dbcontext);
        }

        public async Task<bool> AddDepartmentIfNotExists(Department department)
        {
            return await _dbManager.AddDepartmentIfNotExists(department);
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _dbManager.GetAllDepartmentsAsync();
        }
    }
}