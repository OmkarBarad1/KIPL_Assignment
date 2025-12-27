
using KlingelnbergMachineAssetManagement.Domain;
using KlingelnbergMachineAssetManagement.Domian;
namespace KlingelnbergMachineAssetManagement.Services
{
    public class ApiGetService
    {
        private readonly HttpClient _http;

        public ApiGetService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        public async Task<List<MachineAsset>> GetAllDataAsync()
        {
            return await _http.GetFromJsonAsync<List<MachineAsset>>($"/api/machines/all") ?? new List<MachineAsset>();
        }
        public async Task<List<Asset>> GetAssetByMachineNameAsync(string machineName)
        {
            return await _http.GetFromJsonAsync<List<Asset>>($"/api/machines/{machineName}/assets") ?? new List<Asset>();
            
        }

        public async Task<List<Machine>> GetMachineByAssetNameAsync(string assetName)
        {
            return await _http.GetFromJsonAsync<List<Machine>>($"/api/machines/{assetName}/machines") ?? new List<Machine>();
        }

        public async Task<List<Machine>> GetMachineThatUseLatestSeriesOfAssetAsync()
        {
            return await _http.GetFromJsonAsync<List<Machine>>($"/api/machines/latest") ?? new List<Machine>();
        }
    }

}
