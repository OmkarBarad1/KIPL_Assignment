using KlingelnbergMachineAssetManagement.Api.Domain;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace KlingelnbergMachineAssetManagement.Api.Application.Interfaces
{
    public interface IUploadedMatrixParser
    {
        bool CanHandle(string extension);
        Task<List<MachineAsset>> ParseAsync(IFormFile file);
    }
}
