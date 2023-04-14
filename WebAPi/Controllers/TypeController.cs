using Data.DTO.Type;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : BaseDictionaryController<TypeEntity, TypeDTO, CreateTypeDTO, UpdateTypeDTO, ITypeRepository, ITypeService>
    {
        public TypeController(ITypeService typeService) : base(typeService)
        {
        }
    }
}