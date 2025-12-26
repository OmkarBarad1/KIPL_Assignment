
using KlingelnbergMachineAssetManagement.Domain;
namespace KlingelnbergMachineAssetManagement.Services
{
    public class ApiGetService
    {
        private readonly HttpClient _http;

        public ApiGetService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        public async Task<List<Asset>> GetAssetByMachineName(string machineName)
        {
            List<Asset> result = new List<Asset>();

            return await _http.GetFromJsonAsync<List<Asset>>($"/api/machines/{machineName}/assets") ?? new List<Asset>();
            
        }

        public async Task<List<Machine>> GetMachineByAssetName(string assetName)
        {
            return await _http.GetFromJsonAsync<List<Machine>>($"/api/machines/{assetName}/machines") ?? new List<Machine>();
        }

        public async Task<List<Machine>> GetMachineThatUseLatestSeriesOfAsset()
        {
            return await _http.GetFromJsonAsync<List<Machine>>($"/api/machines/latest") ?? new List<Machine>();
        }
    }

}
