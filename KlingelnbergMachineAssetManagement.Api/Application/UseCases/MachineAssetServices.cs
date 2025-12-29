using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Domain;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace KlingelnbergMachineAssetManagement.Api.Application.UseCases
{
    public class MachineAssetServices : IMachineAssetServices
    {
        private readonly IRepository _repository;
        public MachineAssetServices(IRepository repository)
        {
            _repository = repository;
        }


        public async  Task<List<MachineAsset>> GetAllDataAsync()
        {
            var records = new List<MachineAsset>();

            records = await _repository.GetAllDataAsync();


            return records;
        }


        public async Task<List<Asset>> GetAssetByMachineNameAsync(string machineName)
        {
            var result = new List<Asset>();

            if (string.IsNullOrWhiteSpace(machineName))
                return result;

            var records = new List<MachineAsset>();

            records = await _repository.GetAllDataAsync();

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

            var records = new List<MachineAsset>();

            records = await _repository.GetAllDataAsync();

            var result = new List<Machine>();

            result = records
                .Where(r => r.AssetName.Equals(assetName, StringComparison.OrdinalIgnoreCase))
                .Select(r => r.MachineName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(name => new Machine(name))
                .ToList();

            return result;

        }


        public async Task<List<Machine>> MachinesWithLatestAssetSeriesAsync()
        {

            var records = new List<MachineAsset>();

            records = await _repository.GetAllDataAsync();

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
          result=  machines.Where(mg => mg
           .All(ma => latestSeriesByAsset[ma.AssetName] == ExtractSeriesNumber(ma.Series)))
           .Select (mg=> new Machine(mg.Key))
           .ToList();
                           
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
