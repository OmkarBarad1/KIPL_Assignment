using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Domain;
namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileRead
{
    public class Repository : IRepository
    {
        private readonly IEnumerable<IMatrixParser> _parsers;
        string _filePath;

        public Repository(IEnumerable<IMatrixParser> parsers,string filePath)
        {
            _parsers = parsers;
            _filePath = filePath;
        }
        
        public async Task<List<MachineAsset>>GetAllDataAsync()
        {
            var records = new List<MachineAsset>();
            if (!File.Exists(_filePath))
                return records;

            string extension = Path.GetExtension(_filePath);

            var parser = _parsers.FirstOrDefault(p => p.CanHandle(extension)) ?? throw new NotSupportedException($"Invalid File Type");

            using Stream stream = File.OpenRead(_filePath);

            records = await parser.ParseAsync(stream);

            return records;

        }
        public async Task<List<MachineAsset>>GetAllDataAsync(Stream stream, string extension)
        {
            var records = new List<MachineAsset>();

            var parser = _parsers.FirstOrDefault(p => p.CanHandle(extension)) ?? throw new NotSupportedException($"Invalid File Type");

            records = await parser.ParseAsync(stream);

            return records;

        }

    }
}
