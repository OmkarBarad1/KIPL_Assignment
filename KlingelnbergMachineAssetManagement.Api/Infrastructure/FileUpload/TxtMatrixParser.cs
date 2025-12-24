using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Domain;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload
{
    public class TxtMatrixParser : IUploadedMatrixParser
    {
        public bool CanHandle(string ext) => ext == ".txt" || ext == ".csv";

        public async Task<List<MachineAsset>> ParseAsync(IFormFile file)
        {
            var result = new List<MachineAsset>();

            await using var stream = file.OpenReadStream();

            using var reader = new StreamReader(stream);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                if (parts.Length < 3)
                    continue;

                result.Add(new MachineAsset(
                    parts[0].Trim(),
                    parts[1].Trim(),
                    parts[2].Trim()
                ));
            }

            return result;
        }
    }
}
