using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileParsers
{
    public class JsonMatrixParser : IUploadedMatrixParser
    {
        public bool CanHandle(string ext) => ext == ".json";

        public async Task<List<MachineAsset>> ParseAsync(IFormFile  file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<List<MachineAsset>>(json)
                   ?? new();
        }
    }
}
