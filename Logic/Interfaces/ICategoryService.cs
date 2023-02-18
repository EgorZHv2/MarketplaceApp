using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface ICategoryService : IBaseDictionaryService<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository>
    {
        public Task<bool> CheckParentCategory(Guid categoryId, Guid parentId);

        public Task<List<CategoryDTO>> GetCategoryTree();
    }
}