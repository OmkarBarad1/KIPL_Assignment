using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Domain;
using KlingelnbergMachineAssetManagement.Domian;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace KlingelnbergMachineAssetManagement.Api.Application.UseCases
{
    public class MachineAssetServices : IMachineAssetServices
    {
        private List<MachineAsset> _records= new();
        private readonly IRepository _repository;
        private readonly SemaphoreSlim _lock = new(1, 1);
        private bool _initialized = false;
        public MachineAssetServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task InitializeAsync()
        {
            if (_initialized) return;

            await _lock.WaitAsync();

            try
            {
                if (_initialized) return; 

                _records = await _repository.GetAllDataAsync();
                _initialized = true;
            }
            finally
            {
                _lock.Release();
            }
        }

        public List<MachineAsset> records => _records ?? new();

        public async  Task<List<MachineAsset>> GetAllDataAsync()
        {
            await InitializeAsync();
            return records;
        }


        public async Task<List<Asset>> GetAssetByMachineNameAsync(string machineName)
        {
            var result = new List<Asset>();

            if (string.IsNullOrWhiteSpace(machineName))
                return result;

            await InitializeAsync();

            //result = (from r in records
            //          where r.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase)
            //          group r by new { r.AssetName, r.Series } into g
            //          select new Asset
            //          (
            //            g.Key.AssetName,
            //            g.Key.Series
            //          ))
            //          .ToList();

            result = records
                .Where(r => r.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase))
                .GroupBy(r => new { r.AssetName, r.Series })
                .Select(g => new Asset(
                    g.Key.AssetName,
                    g.Key.Series
                ))
                .ToList();

            return result;

        }

        public async Task<List<Machine>> GetMachineByAssetNameAsync(string assetName)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                return new List<Machine>();

            await InitializeAsync();

            var result = new List<Machine>();

            //result =(from r in records
            //        where r.AssetName.Equals(assetName, StringComparison.OrdinalIgnoreCase)
            //        select r.MachineName
            //        )
            //        .Distinct(StringComparer.OrdinalIgnoreCase)
            //        .Select(name => new Machine(name))
            //        .ToList();


            result = records
                .Where(r => r.AssetName.Equals(assetName, StringComparison.OrdinalIgnoreCase))
                .Select(r => r.MachineName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(name => new Machine(name))
                .ToList();

            return result;

        }


        public async Task<List<Machine>> GetMachineThatUseLatestSeriesOfAssetAsync()
        {


            await InitializeAsync();
            //var latestSeriesByAsset = (from r in records
            //                           group r by r.AssetName into assetGroup
            //                           select new
            //                           {
            //                               AssetName = assetGroup.Key,
            //                               MaxSeries =
            //                             (from x in assetGroup
            //                              select ExtractSeriesNumber(x.Series))
            //                              .Max()
            //                           })
            //                          .ToDictionary(
            //                          x => x.AssetName,
            //                          x => x.MaxSeries
            //                          );

            //var machines = from r in records
            //               group r by r.MachineName;

            var latestSeriesByAsset = records
                .GroupBy(r => r.AssetName)
                .Select(g => new
                {
                    AssetName = g.Key,
                    MaxSeries = g.Select(x => ExtractSeriesNumber(x.Series)).Max()
                })
                .ToDictionary(
                    x => x.AssetName,
                    x => x.MaxSeries
                );

            var machines = records.GroupBy(r => r.MachineName);
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
            if (string.IsNullOrWhiteSpace(series))
                return 0;

            if (series.Length < 2)
                return 0;

            var numberPart = series.Substring(1);

            if (int.TryParse(numberPart, out int value))
                return value;

            return 0;
        }

    }
}
