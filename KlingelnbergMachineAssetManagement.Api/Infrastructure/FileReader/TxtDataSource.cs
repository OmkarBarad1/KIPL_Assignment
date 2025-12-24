using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;    
using KlingelnbergMachineAssetManagement.Api.Domain.Entities;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileReader
{
    public class TxtDataSource : IDataSource
    {
        public IEnumerable<MachineAsset> GetAllData(string filePath)
        {
            var records = new List<MachineAsset>();
            if (!File.Exists(filePath)) return records;

            using var reader = new StreamReader(filePath);
            string? line = reader.ReadLine();
            while (line != null)
            {
                var parts = line.Split(',');
                if (parts.Length < 3) 
                { 
                    line = reader.ReadLine(); 
                    continue; 
                }

                records.Add(new MachineAsset(parts[0].Trim(), parts[1].Trim(), parts[2].Trim()));
                line = reader.ReadLine();
            }
            return records;
        }
    }
}
