using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace KlingelnbergMachineAssetManagement.Services
{
    public class ApiPostService
    {
        private readonly HttpClient _http;

        public ApiPostService(HttpClient http)
        {
            _http = http;
        }

        public async Task UploadAsync(IBrowserFile file, bool replace)
        {
            using var content = new MultipartFormDataContent();

            var stream = file.OpenReadStream();

            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.Name);

            content.Add(new StringContent(replace.ToString()), "replace");

            var response = await _http.PostAsync("api/matrix/upload", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
