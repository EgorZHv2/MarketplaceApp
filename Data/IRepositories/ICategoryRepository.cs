using Data.Entities;

namespace Data.IRepositories
{
    public interface ICategoryRepository : IBaseDictionaryRepository<Category>
    {
        public Task<IEnumerable<Category>> GetCategoriesWithChilds();

        public Task<IEnumerable<Category>> GetCategoriesByParentId(Guid parentId);

        public Task<IEnumerable<Category>> GetCategoriesWithoutParentId();
    }
}