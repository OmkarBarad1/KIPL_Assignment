
using KlingelnbergMachineAssetManagement.Domain;
using KlingelnbergMachineAssetManagement.Domian;


namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IMachineAssetServices
    {
        Task<List<Asset>> GetAssetByMachineNameAsync(string machineName);

        Task<List<Machine>> GetMachineByAssetNameAsync(string assetName);

        Task<List<Machine>> GetMachineThatUseLatestSeriesOfAssetAsync();

        Task<List<MachineAsset>> GetAllDataAsync();
        Task InitializeAsync();
    }
}
