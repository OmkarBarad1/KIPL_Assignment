using KlingelnbergMachineAssetManagement.Api.Entities;

namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IRepository
    {
        Task<List<MachineAsset>> GetAllData(Stream stream, string extension);
    }
}
