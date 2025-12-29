
using KlingelnbergMachineAssetManagement.Domain;


namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IMatrixParser
    {
        bool CanHandle(string extension);
        Task<List<MachineAsset>> ParseAsync(Stream stream);
    }
}
