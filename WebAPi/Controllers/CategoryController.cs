using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseDictionaryController<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository, ICategoryService>
    {
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryTree()
        {
            var result = await _dictionaryService.GetCategoryTree();
            return Ok(result);
        }

        [HttpPut]
        public override async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (model.ParentCategoryId != null)
            {
                var result = await _dictionaryService.CheckParentCategory(model.Id, (Guid)model.ParentCategoryId);
                if (!result)
                {
                    throw new CategoryParentException("Ошибка при выборе родительско категории", "Parent category error");
                }
            }
            await _dictionaryService.Update(UserId, model);
            return Ok();
        }
    }
}