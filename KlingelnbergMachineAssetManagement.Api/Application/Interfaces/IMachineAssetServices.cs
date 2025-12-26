using KlingelnbergMachineAssetManagement.Api.Entities;
using KlingelnbergMachineAssetManagement.Domain;


namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IMachineAssetServices
    {
        Task<List<Asset>> GetAssetByMachineName(string machineName);

        Task<List<Machine>> GetMachineByAssetName(string assetName);

        Task<List<Machine>> GetMachineThatUseLatestSeriesOfAsset();

        Task InitializeAsync();
    }
}
