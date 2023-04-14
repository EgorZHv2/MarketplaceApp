using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseDictionaryController<CategoryEntity, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository, ICategoryService>
    {
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
        }
        /// <summary>
        /// Получить дерево категорий с вложенными категориями
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-tree")]
        [Authorize]
        public async Task<IActionResult> GetCategoryTree()
        {
            var result = await _dictionaryService.GetCategoryTree();
            return Ok(result);
        }

        /// <summary>
        /// Изменить категорию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public override async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            await _dictionaryService.Update(model);
            return Ok();
        }
    }
}