
using KlingelnbergMachineAssetManagement.Domain;


namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IMachineAssetServices
    {
        Task<List<Asset>> GetAssetByMachineNameAsync(string machineName);

        Task<List<Machine>> GetMachineByAssetNameAsync(string assetName);

        Task<List<Machine>> MachinesWithLatestAssetSeriesAsync();

        Task<List<MachineAsset>> GetAllDataAsync();
    }
}
