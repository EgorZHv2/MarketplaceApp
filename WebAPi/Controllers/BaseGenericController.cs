﻿using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeActivity([FromBody] EntityActivityDTO model)
        {
            var result = await _service.ChangeActivity(UserId, model);
            return Ok(result);
        }

        
    }
}