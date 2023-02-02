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

        public async Task<IQueryable<Category>> GetCategoriesWithChilds()
        {
            var result = _context.Categories.Include(e => e.Categories).AsNoTracking().AsQueryable();
            return result;
        }
    }
}