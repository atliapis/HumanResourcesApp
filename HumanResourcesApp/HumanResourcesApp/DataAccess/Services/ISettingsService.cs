using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess.Services
{
    internal interface ISettingsService
    {
        Task AddSetting(string name, int value);

        Task<int> GetResultsPerPageSetting();
    }
}
