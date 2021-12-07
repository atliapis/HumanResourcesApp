using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess.Services
{
    public class SettingsService: ISettingsService
    {
        private readonly DatabaseAccessManager _dbManager;

        public SettingsService(HumanResourcesContext dbcontext)
        {
            _dbManager = new DatabaseAccessManager(dbcontext);
        }

        public async Task AddSetting(string name, int value)
        {
            await _dbManager.AddSetting(name, value);
        }

        public async Task<int> GetResultsPerPageSetting()
        {
            return await _dbManager.GetResultsPerPageSetting();
        }
    }
}