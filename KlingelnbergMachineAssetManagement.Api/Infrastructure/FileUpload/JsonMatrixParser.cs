using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Entities;
using System.Text.Json;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload
{
    public class JsonMatrixParser : IMatrixParser
    {
        public bool CanHandle(string ext) => ext == ".json";

        public async Task<List<MachineAsset>> ParseAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<List<MachineAsset>>(json)
                   ?? new();
        }
    }
}
