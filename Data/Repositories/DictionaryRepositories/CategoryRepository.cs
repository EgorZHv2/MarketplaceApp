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

        public async Task<IEnumerable<Category>> GetCategoriesWithChilds(CancellationToken cancellationToken = default)
        {
            var result = await _dbset.Include(e => e.Categories).ToListAsync(cancellationToken);
            return result;
        }
        public async Task<IEnumerable<Category>> GetCategoriesByParentId(Guid parentId,CancellationToken cancellationToken = default)
        {
            var result = await _dbset.Where(e => e.ParentCategoryId == parentId).ToListAsync(cancellationToken);
            return result;
        }
        public async Task<IEnumerable<Category>> GetCategoriesWithoutParentId(CancellationToken cancellationToken = default)
        {
            var result = await _dbset.Where(e => e.ParentCategoryId == null).ToListAsync(cancellationToken);
            return result;
        }
    }
}