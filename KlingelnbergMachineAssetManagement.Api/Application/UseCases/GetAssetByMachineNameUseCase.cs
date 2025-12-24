using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;

namespace KlingelnbergMachineAssetManagement.Api.Application.UseCases
{
    public class GetAssetByMachineNameUseCase
    {
        private readonly IDataSource _dataSource;
        private readonly IMatrixFileLocator _fileLocator;

        public GetAssetByMachineNameUseCase(IDataSource dataSource, IMatrixFileLocator fileLocator)
        {
            this._dataSource = dataSource;
            this._fileLocator = fileLocator;
        }

        public List<string> GetAssetByMachineName(string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                return new List<string>();
            var filePath = _fileLocator.GetMatrixFilePath();
            if (filePath == null)
            {
                return new List<string>();
            }


            var records = _dataSource.GetAllData(filePath);

            return (from r in records
                    where r.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase)
                    select r.AssetName)
                    .Distinct()
                    .ToList();
        }
    }
}
