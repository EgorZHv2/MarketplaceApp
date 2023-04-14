using Data.DTO;
using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Data.Repositories.Repositories
{
    public class UsersFavoriteShopsRepository : IUsersFavoriteShopsRepository
    {
        private ApplicationDbContext _context;
        private DbSet<UserFavoriteShopEntity> _dbset => _context.Set<UserFavoriteShopEntity>();

        public UsersFavoriteShopsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserFavoriteShopEntity?> GetFavByShopAndUserId(Guid userId, Guid shopId)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.UserId == userId && e.ShopId == shopId);
        }

        public async Task<ICollection<ShopEntity>> GetFavoriteShopsByUserId(Guid userId)
        {
            return await _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop)
                .ToListAsync();
        }

        public async Task Delete(UserFavoriteShopEntity entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PageModelDTO<ShopEntity>> GetFavsPageByUserId(Guid userId, PaginationDTO pagination)
        {
            var queryable = _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop);

            var result = await queryable.ToPageModelAsync<ShopEntity>(pagination);
            return result;
        }
    }
}