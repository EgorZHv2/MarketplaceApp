using Data.DTO;
using Data.DTO.Type;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : BaseDictionaryController<Data.Entities.Type, TypeDTO, CreateTypeDTO,UpdateTypeDTO,ITypeRepository,ITypeService>
    {
        public TypeController(ITypeService typeService) : base(typeService)
        {
        }
    }
}