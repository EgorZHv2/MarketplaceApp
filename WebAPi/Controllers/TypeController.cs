using Data.DTO;
using Data.Entities;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : BaseDictionaryController<Data.Entities.Type,TypeDTO>
    {
        public TypeController(IBaseDictionaryService<Data.Entities.Type,TypeDTO> dictionaryService) : base(dictionaryService) { }
    }
}
