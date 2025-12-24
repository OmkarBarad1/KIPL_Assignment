using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace KlingelnbergMachineAssetManagement.Api.Controllers
{
    [ApiController]
    [Route("api/machines")]
    public class GetAssetController : ControllerBase
    {
        private readonly GetAssetByMachineNameUseCase _getAssetByMachineNameUseCase;

        public GetAssetController(GetAssetByMachineNameUseCase getAssetByMachineNameUseCase)
        {
            _getAssetByMachineNameUseCase = getAssetByMachineNameUseCase;
        }

        [HttpGet("{machineName}/assets")]
        public IActionResult GetAssetsByMachine(string machineName)
        {
            var result = _getAssetByMachineNameUseCase
                .GetAssetByMachineName(machineName.Trim());

            return Ok(result);
        }
    }
}
