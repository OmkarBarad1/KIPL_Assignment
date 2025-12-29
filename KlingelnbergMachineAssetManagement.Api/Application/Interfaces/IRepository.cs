
using KlingelnbergMachineAssetManagement.Domain;

namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IRepository
    {
        Task<List<MachineAsset>> GetAllDataAsync();
        Task<List<MachineAsset>> GetAllDataAsync(Stream stream, string extension);
    }
}
