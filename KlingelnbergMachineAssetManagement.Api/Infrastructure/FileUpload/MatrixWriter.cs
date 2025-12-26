
using KlingelnbergMachineAssetManagement.Api.Entities;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.Fileupload
{
    public class MatrixWriter
    {
        private readonly string _filePath;

        public MatrixWriter(string filepath)
        {
            _filePath = filepath;
        }

        public void Save(List<MachineAsset> records)
        {
            if (string.IsNullOrWhiteSpace(_filePath))
                throw new ArgumentException("File path cannot be empty", nameof(_filePath));

           
            using var writer = new StreamWriter(_filePath, append : false);

            foreach (var r in records)
            {
                writer.WriteLine($"{r.MachineName}, {r.AssetName}, {r.Series}");
            }
        }
    }

}
