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

        [HttpGet("{machineName}/assets")]
        public async Task<IActionResult> GetAssetsByMachine(string machineName)
        {
            var result = await _Services.GetAssetByMachineName(machineName.Trim());

            return Ok(result);
        }

        [HttpGet("{assetName}/machines")]
        public async Task<IActionResult> GetMachinesByAsset(string assetName)
        {
            var result = await _Services.GetMachineByAssetName(assetName.Trim());

            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetMachinesUsingLatestAssetSeries()
        {
            var result = await _Services.GetMachineThatUseLatestSeriesOfAsset();
            return Ok(result);
        }
    }
}
