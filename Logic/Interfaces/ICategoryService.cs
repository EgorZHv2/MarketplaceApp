using Data.DTO;
using Data.Entities;

namespace Logic.Interfaces
{
    public interface ICategoryService : IBaseDictionaryService<Category, CreateCategoryDTO, CategoryDTO>
    {
        public Task<bool> CheckParentCategory(Guid categoryid, Guid parentid);

        public Task<List<CategoryDTO>> GetCategoryTree();
    }
}