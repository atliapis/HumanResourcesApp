using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess.Services
{
    internal interface IStatusesService
    {
        Task<bool> AddStatusIfNotExists(Status status);

        Task<List<Status>> GetAllStatusesAsync();
    }
}
