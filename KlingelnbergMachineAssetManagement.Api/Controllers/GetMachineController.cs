using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace KlingelnbergMachineAssetManagement.Api.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class GetMachineController : ControllerBase
    {
        private readonly GetMachineByAssetNameUseCase _getMachineByAssetNameUseCase;

        public GetMachineController(GetMachineByAssetNameUseCase getMachineByAssetNameUseCase)
        {
            _getMachineByAssetNameUseCase = getMachineByAssetNameUseCase;
        }

        [HttpGet("{assetName}/machines")]
        public IActionResult GetMachinesByAsset(string assetName)
        {
            var result = _getMachineByAssetNameUseCase
                .GetMachineByAssetName(assetName.Trim());

            return Ok(result);
        }
    }
}
