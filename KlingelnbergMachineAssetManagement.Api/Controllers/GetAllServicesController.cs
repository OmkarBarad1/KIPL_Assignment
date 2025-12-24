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
        public IActionResult GetAssetsByMachine(string machineName)
        {
            var result = _Services.GetAssetByMachineName(machineName.Trim());

            return Ok(result);
        }

        [HttpGet("{assetName}/machines")]
        public IActionResult GetMachinesByAsset(string assetName)
        {
            var result = _Services.GetMachineByAssetName(assetName.Trim());

            return Ok(result);
        }

        [HttpGet("latest")]
        public IActionResult GetMachinesUsingLatestAssetSeries()
        {
            var result = _Services.GetMachineThatUseLatestSeriesOfAsset();
            return Ok(result);
        }
    }
}
