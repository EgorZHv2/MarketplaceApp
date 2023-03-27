using Data.DTO;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AATestController : ControllerBase
    {
        private readonly IFileHttpService _fileHttpService;

        public AATestController(IFileHttpService fileHttpService)
        {
            _fileHttpService = fileHttpService;
        }
        [HttpPost]
        public async Task PostTest([FromForm] CreateFileDTO createFileDTO)
        {
            await _fileHttpService.PostAsync(createFileDTO);
        }
    }
}
