using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseDictionaryController<CategoryEntity, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository, ICategoryService>
    {
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategoryTree()
        {
            var result = await _dictionaryService.GetCategoryTree();
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public override async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
       
            await _dictionaryService.Update(UserId, model);
            return Ok();
        }
    }
}