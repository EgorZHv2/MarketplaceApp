using Data.DTO;
using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories.DictionaryRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseDictionaryController<TEntity, TDTO, TCreateDTO,TUpdateDTO,TRepository,TService> : BaseController 
        where TEntity : BaseDictionaryEntity 
        where TDTO : BaseDictinoaryOutputDTO
        where TCreateDTO:BaseDictionaryCreateDTO
        where TUpdateDTO : BaseDictionaryUpdateDTO
        where TRepository:IBaseDictionaryRepository<TEntity>
        where TService : IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>

    {
        protected TService _dictionaryService;

        public BaseDictionaryController(TService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TCreateDTO model)
        {
            await _dictionaryService.Create(UserId, model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public virtual async Task<IActionResult> Update(TUpdateDTO model)
        {
            await _dictionaryService.Update(UserId, model);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPage([FromQuery] FilterPagingModel pagingModel)
        {
            var result = await _dictionaryService.GetPage(pagingModel);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dictionaryService.Delete(UserId, id);
            return Ok();
        }
    }
}