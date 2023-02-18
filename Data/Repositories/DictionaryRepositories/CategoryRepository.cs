using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.DictionaryRepositories
{
    public class CategoryRepository : BaseDictionaryRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithChilds()
        {
            var result = await _dbset.Include(e => e.Categories).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByParentId(Guid parentId)
        {
            var result = await _dbset.Where(e => e.ParentCategoryId == parentId).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithoutParentId()
        {
            var result = await _dbset.Where(e => e.ParentCategoryId == null).ToListAsync();
            return result;
        }
    }
}