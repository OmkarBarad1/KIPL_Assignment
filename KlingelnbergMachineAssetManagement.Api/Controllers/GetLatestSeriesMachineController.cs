using KlingelnbergMachineAssetManagement.Api.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace KlingelnbergMachineAssetManagement.Api.Controllers
{
    [ApiController]
    [Route("api/machines")]
    public class GetLatestSeriesMachineController : ControllerBase
    {
        private readonly GetMachineThatUseLatestSeriesOfAssetUseCase _useCase;

        public GetLatestSeriesMachineController(GetMachineThatUseLatestSeriesOfAssetUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("latest")]
        public IActionResult GetMachinesUsingLatestAssetSeries()
        {
            var result = _useCase.GetMachineThatUseLatestSeriesOfAsset();
            return Ok(result);
        }
    }
}
