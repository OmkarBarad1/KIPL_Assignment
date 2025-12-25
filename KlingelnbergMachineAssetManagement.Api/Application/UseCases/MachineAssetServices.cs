using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Domain;

namespace KlingelnbergMachineAssetManagement.Api.Application.UseCases
{
    public class MachineAssetServices : IMachineAssetServices
    {

        string _filePath;

        public MachineAssetServices(string filePath)
        {
            _filePath = filePath;
        }

        public List<Asset> GetAssetByMachineName(string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                return new List<Asset>();

            var records = GetAllData();
            var result = new List<Asset>();

            result = (from r in records
                          where r.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase)
                          group r by new { r.AssetName, r.Series } into g
                          select new Asset
                          (
                               g.Key.AssetName,
                               g.Key.Series
                          ))
                          .ToList();

            return result;

        }

        public List<Machine> GetMachineByAssetName(string assetName)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                return new List<Machine>();

            var records = GetAllData();

            var result = new List<Machine>();

            result = (from r in records
                      where r.AssetName.Equals(assetName, StringComparison.OrdinalIgnoreCase)
                      select new Machine
                      (
                          r.MachineName
                       ))
                      .Distinct()
                      .ToList();

            return result;

        }


        public List<Machine> GetMachineThatUseLatestSeriesOfAsset()
        {


            var records = GetAllData();

            var latestSeriesByAsset = (from r in records
                                       group r by r.AssetName into assetGroup
                                       select new
                                       {
                                           AssetName = assetGroup.Key,
                                           MaxSeries =
                                         (from x in assetGroup
                                          select ExtractSeriesNumber(x.Series))
                                          .Max()
                                       })
                                      .ToDictionary(
                                      x => x.AssetName,
                                      x => x.MaxSeries
                                      );

            var machines = from r in records
                           group r by r.MachineName;

            List<Machine> result = new();

            foreach (var machineGroup in machines)
            {
                bool usesAllLatest = true;

                foreach (var record in machineGroup)
                {
                    int recordSeries = ExtractSeriesNumber(record.Series);
                    int latestSeries = latestSeriesByAsset[record.AssetName];

                    if (recordSeries < latestSeries)
                    {
                        usesAllLatest = false;
                        break;
                    }
                }

                if (usesAllLatest)
                {
                    result.Add(new Machine(machineGroup.Key));
                }
            }

            return result;
        }

        private int ExtractSeriesNumber(string series)
        {
            return int.Parse(series.Substring(1));
        }


        public List<MachineAsset> GetAllData()
        {

            var records = new List<MachineAsset>();

            if (!File.Exists(_filePath)) return records;

            using var reader = new StreamReader(_filePath);
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
