using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.DictionaryRepositories
{
    public class CategoryRepository : BaseDictionaryRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

    

        public async Task<IEnumerable<CategoryEntity>> GetCategoriesByParentId(Guid parentId)
        {
            var result = await _dbset.Where(e => e.ParentCategoryId == parentId).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CategoryEntity>> GetCategoriesWithoutParentId()
        {
            var result = await _dbset.Where(e => e.ParentCategoryId == null).ToListAsync();
            return result;
        }

        public async Task<CategoryEntity> GetCategoryByName(string name)
        {
            var result = await _dbset.FirstOrDefaultAsync(e=>e.Name == name);
            return result;
        }
    }
}