using Data.DTO;
using Data.Options;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml.Table;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AATestFileController : ControllerBase
    {
        private readonly IHttpService _httpService;
        private readonly ApplicationOptions _options;
        public AATestFileController(IHttpService service,IOptions<ApplicationOptions> options)
        {
            _httpService= service;
            _options = options.Value;
        }
        [HttpPost]
        public async Task<IActionResult> TestFileCreate([FromForm] CreateFileDTO createFileDTO)
        {
            var result = await _httpService.PostAsync<FileInfoDTO>(_options.FileControllerPath, createFileDTO);
            return Ok(result);
        }
    }
}
