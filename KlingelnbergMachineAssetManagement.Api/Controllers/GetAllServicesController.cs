using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace KlingelnbergMachineAssetManagement.Api.Controllers
{
    [ApiController]
    [Route("api/machines")]
    public class GetAllServicesController : ControllerBase
    {
        private readonly IMachineAssetServices _Services;

        public GetAllServicesController(IMachineAssetServices services)
        {
            _Services = services;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDataAsync()
        {
            var result = await _Services.GetAllDataAsync();

            return Ok(result);
        }

        [HttpGet("{machineName}/assets")]
        public async Task<IActionResult> GetAssetsByMachineAsync(string machineName)
        {
            var result = await _Services.GetAssetByMachineNameAsync(machineName.Trim());

            return Ok(result);
        }

        [HttpGet("{assetName}/machines")]
        public async Task<IActionResult> GetMachinesByAssetAsync(string assetName)
        {
            var result = await _Services.GetMachineByAssetNameAsync(assetName.Trim());

            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetMachinesUsingLatestAssetSeriesAsync()
        {
            var result = await _Services.GetMachineThatUseLatestSeriesOfAssetAsync();
            return Ok(result);
        }
    }
}
