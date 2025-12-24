using KlingelnbergMachineAssetManagement.Api.Domain.Entities;

namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IDataSource
    {
        IEnumerable<MachineAsset> GetAllData(string _filePath);
    }
}
