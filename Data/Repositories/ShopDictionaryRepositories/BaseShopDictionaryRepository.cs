using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public abstract class BaseShopDictionaryRepository<TEntity> : IBaseShopDictionaryRepository<TEntity>
        where TEntity : BaseShopDictionaryEntity
    {
        private ApplicationDbContext _context;
        protected DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public BaseShopDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateRange(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllByShop(ShopEntity shop)
        {
            var list = await _dbSet.Where(e => e.ShopId == shop.Id).ToListAsync();
            _dbSet.RemoveRange(list);
            await _context.SaveChangesAsync();
        }

       
        public async Task UpdateRange(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}