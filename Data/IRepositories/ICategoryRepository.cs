using Data.Entities;

namespace Data.IRepositories
{
    public interface ICategoryRepository : IBaseDictionaryRepository<CategoryEntity>
    {
        

        public Task<IEnumerable<CategoryEntity>> GetCategoriesByParentId(Guid parentId);

        public Task<IEnumerable<CategoryEntity>> GetCategoriesWithoutParentId();
        Task<CategoryEntity?> GetCategoryByName(string name);
    }
}