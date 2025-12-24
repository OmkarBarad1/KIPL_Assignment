
using KlingelnbergMachineAssetManagement.Api.Domain.Entities;
namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileWriter
{
    public class MatrixWriter
    {
        private readonly string _path;

        public MatrixWriter(IWebHostEnvironment env)
        {
            _path = Path.Combine(
                env.ContentRootPath,
                "..",
                "KlingelnbergMachineAssetManagement.Api",
                "Data",
                "matrix.txt"
            );
        }

        public void Save(List<MachineAsset> records, bool append)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);

            using var writer = new StreamWriter(_path, append);

            foreach (var r in records)
            {
                writer.WriteLine($"{r.MachineName}, {r.AssetName}, {r.Series}");
            }
        }
    }

}
