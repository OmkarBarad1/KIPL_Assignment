using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Entities;
namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileRead
{
    public class Repository : IRepository
    {
        private readonly IEnumerable<IMatrixParser> _parsers;

        public Repository(IEnumerable<IMatrixParser> parsers)
        {
            _parsers = parsers;
        }

        public async Task<List<MachineAsset>>GetAllData(Stream stream, string extension)
        {
            var records = new List<MachineAsset>();

            var parser = _parsers.FirstOrDefault(p => p.CanHandle(extension)) ?? throw new NotSupportedException($"Invalid File Type");

            records = await parser.ParseAsync(stream);

            return records;

        }
    }
}
