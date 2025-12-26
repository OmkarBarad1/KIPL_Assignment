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
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                await using Stream stream = file.OpenReadStream();
                await _service.UploadAsync(stream, extension);

                return Ok(new
                {
                    message = "File Uploded Succesfully"
                });
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new
                {
                   error = ex.Message
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
