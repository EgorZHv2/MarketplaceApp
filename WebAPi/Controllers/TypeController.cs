using Data.DTO;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : BaseDictionaryController<Data.Entities.Type, TypeDTO, TypeDTO>
    {
        public TypeController(ITypeService typeService) : base(typeService)
        {
        }
    }
}