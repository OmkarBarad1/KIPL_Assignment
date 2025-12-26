using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace KlingelnbergMachineAssetManagement.Services
{
    public class ApiPostService
    {
        private readonly HttpClient _http;

        public ApiPostService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        public async Task UploadAsync(IBrowserFile file)
        {
            using var content = new MultipartFormDataContent();

            var stream = file.OpenReadStream();

            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.Name);

            var response = await _http.PostAsync($"api/matrix/upload", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
    }
}
