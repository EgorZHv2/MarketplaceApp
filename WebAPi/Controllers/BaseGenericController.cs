using Data.DTO;
using Data.DTO.BaseDTOs;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseGenericController<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository, TService> : BaseController
        where TService : IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        where TEntity : BaseEntity
        where TUpdateDTO : BaseUpdateDTO
        where TRepository : IBaseRepository<TEntity>
    {
        protected TService _service { get; set; }

        public BaseGenericController(TService service)
        {
            _service = service;
        }
        /// <summary>
        /// Сменить активность у сущности.
        /// </summary>
        /// <param name="model">Модель смены активности</param>
        /// <returns></returns>
        [HttpPatch("change-activity")]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> ChangeActivity([FromBody] EntityActivityDTO model)
        {
            var result = await _service.ChangeActivity(UserId, model);
            return Ok(result);
        }
    }
}