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

        public async Task<UserFavoriteShop> GetFavByShopAndUserId(Guid userId, Guid shopId, CancellationToken cancellationToken = default)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.UserId == userId && e.ShopId == shopId, cancellationToken);
        }

        public async Task<ICollection<Shop>> GetFavoriteShopsByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop)
                .ToListAsync(cancellationToken);
        }

        public async Task Delete(UserFavoriteShop entity, CancellationToken cancellationToken = default)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PageModelDTO<Shop>> GetFavsPageByUserId(Guid userId, int pagenumber, int pagesize, CancellationToken cancellationToken = default)
        {
            var queryable = _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop);

            var result = await queryable.ToPageModelAsync<Shop>(pagenumber, pagesize, cancellationToken);
            return result;
        }
    }
}