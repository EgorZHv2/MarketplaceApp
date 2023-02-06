using Data.Entities;

namespace Data.IRepositories
{
    public interface ICategoryRepository : IBaseDictionaryRepository<Category>
    {
        public Task<IEnumerable<Category>> GetCategoriesWithChilds(CancellationToken cancellationToken = default);
        public  Task<IEnumerable<Category>> GetCategoriesByParentId(Guid parentId, CancellationToken cancellationToken = default);
        public  Task<IEnumerable<Category>> GetCategoriesWithoutParentId(CancellationToken cancellationToken = default);
    }
}