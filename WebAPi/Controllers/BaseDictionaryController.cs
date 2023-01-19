using Data.DTO;
using Data.Entities;
using Data.Models;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseDictionaryController<TEntity,TDTO>:BaseController where TEntity : BaseDictionaryEntity where TDTO: DictionaryDTO
    {
        private IBaseDictionaryService<TEntity,TDTO> _dictionaryService;
        
        public BaseDictionaryController(IBaseDictionaryService<TEntity,TDTO> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(TDTO model)
        {
           
            _dictionaryService.Create(UserId, model);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(TDTO model)
        {
            
            _dictionaryService.Update(UserId, model);
            return Ok();
        }
        [HttpGet] 
        [Authorize]
        public async Task<IActionResult> GetPage([FromQuery] FilterPagingModel pagingModel)
        {
           
            var result = _dictionaryService.GetPage(pagingModel);
            return Ok(result);
        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _dictionaryService.Delete(UserId, id);
            return Ok();
        }

    }
}
