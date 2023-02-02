using Data.Entities;

namespace Data.IRepositories
{
    public interface ICategoryRepository : IBaseDictionaryRepository<Category>
    {
        public Task<IQueryable<Category>> GetCategoriesWithChilds();
    }
}