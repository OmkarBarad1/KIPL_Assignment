using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileCheck;

namespace KlingelnbergMachineAssetManagement.Api.Application.UseCases
{
    public class GetMachineByAssetNameUseCase
    {
        private readonly IDataSource _dataSource;
        private readonly IMatrixFileLocator _fileLocator;

        public GetMachineByAssetNameUseCase(IDataSource dataSource, IMatrixFileLocator fileLocator)
        {
            this._dataSource =  dataSource;
            this._fileLocator = fileLocator;

        }

        public List<string> GetMachineByAssetName(string assetName)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                return new List<string>();

            var filePath = _fileLocator.GetMatrixFilePath();
            if (filePath == null) return new List<string>();

            //var dataSource = _fileChecker.Create(filePath);
            var records = _dataSource.GetAllData(filePath);

            var result = new List<string>();

            result = (from r in records
                      where r.AssetName.Equals(assetName, StringComparison.OrdinalIgnoreCase)
                      select r.MachineName)
                      .Distinct()
                      .ToList();

            return result;

        }
    }
}
