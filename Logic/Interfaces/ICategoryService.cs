using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface ICategoryService : IBaseDictionaryService<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository>
    {
        public Task<bool> CheckParentCategory(Guid categoryid, Guid parentid, CancellationToken cancellationToken = default);

        public Task<List<CategoryDTO>> GetCategoryTree(CancellationToken cancellationToken = default);
    }
}