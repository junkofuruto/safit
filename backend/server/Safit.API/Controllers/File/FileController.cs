using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.FileServer;

[Authorize]
[Route("fs")]
[ApiController]
public class FileController : ControllerBase
{
    private IFileService fileService;

    public FileController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetAsync([FromRoute] string name, CancellationToken ct)
    {
        try
        {
            var data = await fileService.GetAsync(name, ct);
            return Ok(data);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("{name}/info")]
    public async Task<IActionResult> GetInfoAsync([FromRoute] string name, CancellationToken ct)
    {
        try
        {
            var fileInfo = await fileService.GetFileInfoAsync(name, ct);
            return Ok(ResponseContract<Core.Domain.Service.Entities.FileInfo>.Create(fileInfo));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<Core.Domain.Service.Entities.FileInfo>.Create(ex));
        }
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync([FromHeader(Name = "Content-Type")] string contentType, CancellationToken ct)
    {
        try
        {
            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                Request.Body.CopyTo(memoryStream);
                data = memoryStream.ToArray();
            }
            var response = await fileService.UploadFileAsync(data, contentType, ct);
            return Ok(ResponseContract<Core.Domain.Service.Entities.FileInfo>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<Core.Domain.Service.Entities.FileInfo>.Create(ex));
        }
    }
}
