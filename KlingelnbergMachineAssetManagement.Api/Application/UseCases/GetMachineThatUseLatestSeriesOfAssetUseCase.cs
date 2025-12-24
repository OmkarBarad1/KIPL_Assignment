using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;

namespace KlingelnbergMachineAssetManagement.Api.Application.UseCases
{
    public class GetMachineThatUseLatestSeriesOfAssetUseCase
    {
        private readonly IDataSource _dataSource;
        private readonly IMatrixFileLocator _fileLocator;
        
        public GetMachineThatUseLatestSeriesOfAssetUseCase(IDataSource dataSource, IMatrixFileLocator fileLocator)
        {
            this._dataSource = dataSource;
            this._fileLocator = fileLocator;

        }

        public List<string> GetMachineThatUseLatestSeriesOfAsset()
        {
            var filePath = _fileLocator.GetMatrixFilePath();
            if (filePath == null) return new List<string>();

            var records = _dataSource.GetAllData(filePath);

            var latestSeriesByAsset =(from r in records
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

            List<string> result = new();

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
                    result.Add(machineGroup.Key);
                }
            }

            return result;
        }

        private int ExtractSeriesNumber(string series)
        {
            return int.Parse(series.Substring(1));
        }
    }
}
