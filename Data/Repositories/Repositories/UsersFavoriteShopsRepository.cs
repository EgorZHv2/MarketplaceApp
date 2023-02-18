using Data.DTO;
using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Repositories
{
    public class UsersFavoriteShopsRepository : IUsersFavoriteShopsRepository
    {
        private ApplicationDbContext _context;
        private DbSet<UserFavoriteShop> _dbset => _context.Set<UserFavoriteShop>();

        public UsersFavoriteShopsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserFavoriteShop?> GetFavByShopAndUserId(Guid userId, Guid shopId)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.UserId == userId && e.ShopId == shopId);
        }

        public async Task<ICollection<Shop>> GetFavoriteShopsByUserId(Guid userId)
        {
            return await _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop)
                .ToListAsync();
        }

        public async Task Delete(UserFavoriteShop entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PageModelDTO<Shop>> GetFavsPageByUserId(Guid userId, int pagenumber, int pagesize)
        {
            var queryable = _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop);

            var result = await queryable.ToPageModelAsync<Shop>(pagenumber, pagesize);
            return result;
        }
    }
}