using KlingelnbergMachineAssetManagement.Api.Domain;


namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IMachineAssetServices
    {
        List<string> GetAssetByMachineName(string machineName);

        List<string> GetMachineByAssetName(string assetName);

        List<string> GetMachineThatUseLatestSeriesOfAsset();

        List<MachineAsset> GetAllData();

    }
}
