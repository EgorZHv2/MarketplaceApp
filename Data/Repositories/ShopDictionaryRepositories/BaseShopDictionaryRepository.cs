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
    public abstract class BaseShopDictionaryRepository<TEntity>:IBaseShopDictionaryRepository<TEntity>
        where TEntity:BaseShopDictionaryEntity
    {
        private ApplicationDbContext _context;
        protected DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public BaseShopDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateRange(CancellationToken cancellationToken = default, params TEntity[] entities)
        {
           await _dbSet.AddRangeAsync(entities, cancellationToken);
           await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAllByShop(Shop shop,CancellationToken cancellationToken = default)
        {
            var list = await _dbSet.Where(e => e.ShopId == shop.Id).ToListAsync(cancellationToken);
            _dbSet.RemoveRange(list);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
            
    }
}
