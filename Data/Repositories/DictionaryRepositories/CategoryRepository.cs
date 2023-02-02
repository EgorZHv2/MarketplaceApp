using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryRepository:BaseDictionaryRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context):base(context) { }
        
        public async Task<IQueryable<Category>> GetCategoriesWithChilds()
        {
            var result = _context.Categories.Include(e=>e.Categories).AsNoTracking().AsQueryable();
            return result;
        }
    }
}
