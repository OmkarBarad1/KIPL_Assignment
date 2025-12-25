using KlingelnbergMachineAssetManagement.Api.Domain;


namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IMachineAssetServices
    {
        List<Asset> GetAssetByMachineName(string machineName);

        List<Machine> GetMachineByAssetName(string assetName);

        List<Machine> GetMachineThatUseLatestSeriesOfAsset();

        List<MachineAsset> GetAllData();

    }
}
