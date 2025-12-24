
using KlingelnbergMachineAssetManagement.Api.Domain;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.Fileupload
{
    public class MatrixWriter
    {
        private readonly string _filepath;

        public MatrixWriter(string filepath)
        {
            _filepath = filepath;
        }

        public void Save(List<MachineAsset> records)
        {

            using var writer = new StreamWriter(_filepath, append : false);

            foreach (var r in records)
            {
                writer.WriteLine($"{r.MachineName}, {r.AssetName}, {r.Series}");
            }
        }
    }

}
