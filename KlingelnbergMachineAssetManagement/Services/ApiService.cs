namespace KlingelnbergMachineAssetManagement.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        public async Task<List<string>> GetAssetByMachineName(string machineName)
        {
            List<string> result = new List<string>();

            return await _http.GetFromJsonAsync<List<string>>($"/api/machines/{machineName}/assets") ?? new List<string>();
            
        }

        public async Task<List<string>> GetMachineByAssetName(string assetName)
        {
            return await _http.GetFromJsonAsync<List<string>>($"/api/machines/{assetName}/machines") ?? new List<string>();
        }

        public async Task<List<string>> GetMachineThatUseLatestSeriesOfAsset()
        {
            return await _http.GetFromJsonAsync<List<string>>($"/api/machines/latest") ?? new List<string>();
        }
    }

}
