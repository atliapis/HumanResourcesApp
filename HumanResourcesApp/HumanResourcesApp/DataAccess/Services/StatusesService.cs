using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess.Services
{
    public class StatusesService: IStatusesService
    {
        private readonly DatabaseAccessManager _dbManager;

        public StatusesService(HumanResourcesContext dbContext)
        {
            _dbManager = new DatabaseAccessManager(dbContext);
        }

        public async Task<bool> AddStatusIfNotExists(Status status)
        {
            return await _dbManager.AddStatusIfNotExists(status);
        }

        public async Task<List<Status>> GetAllStatusesAsync()
        {
            return await _dbManager.GetAllStatusesAsync();
        }
    }
}