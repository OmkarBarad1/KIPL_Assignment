using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace KlingelnbergMachineAssetManagement.Api.Controllers
{
    [ApiController]
    [Route("api/matrix")]
    public class MatrixUploadController : ControllerBase
    {
        private readonly MatrixUploadService _service;

        public MatrixUploadController(MatrixUploadService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                await _service.UploadAsync(file);

                return Ok(new
                {
                    message = "File Uploded Succesfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }
    }
}
