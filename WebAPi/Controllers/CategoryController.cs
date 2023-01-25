﻿using AutoMapper;
using Data;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseDictionaryController<Category,CreateCategoryDTO,CategoryDTO>
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) : base(categoryService) 
        {
            _categoryService = categoryService;
        }
       
           
        

        [HttpGet]
        public async Task<IActionResult> GetCategoryTree()
        {

            var result = await _categoryService.GetCategoryTree();
            return Ok(result);
        }
        [HttpPut]
        public override async Task<IActionResult> Update(CreateCategoryDTO model)
        {
          var result = await  _categoryService.CheckParentCategory(model.Id, (Guid)model.ParentCategoryId);
            if (!result)
            {
                throw new CategoryParentException("Ошибка при выборе родительско категории", "Parent category error");
            }
             _dictionaryService.Update(UserId, model);
            return Ok();
        }
        
    }
}
