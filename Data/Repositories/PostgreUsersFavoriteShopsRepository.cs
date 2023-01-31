using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PostgreUsersFavoriteShopsRepository:BaseRepository<UserFavoriteShop>,IUsersFavoriteShopsRepository
    {
        public PostgreUsersFavoriteShopsRepository(ApplicationDbContext context):base(context) { }

        public async Task<UserFavoriteShop> GetFavByShopAndUserId(Guid userId, Guid shopId,CancellationToken cancellationToken = default)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.UserId == userId && e.ShopId == shopId,cancellationToken);
        }
        public async Task<ICollection<Shop>> GetFavoriteShopsByUserId(Guid userId,CancellationToken cancellationToken = default)
        {
            return await _dbset
                .Include(e => e.Shop)
                .Where(e => e.UserId == userId)
                .Select(p => p.Shop)
                .ToListAsync(cancellationToken);

        }
                
    }
}
