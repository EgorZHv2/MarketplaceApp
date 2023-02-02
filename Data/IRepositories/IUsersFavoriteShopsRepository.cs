using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IUsersFavoriteShopsRepository
    {
        public Task<ICollection<Shop>> GetFavoriteShopsByUserId(Guid userId, CancellationToken cancellationToken = default);
        public Task<UserFavoriteShop> GetFavByShopAndUserId(Guid userId, Guid shopId, CancellationToken cancellationToken = default);
        public Task Delete(UserFavoriteShop entity, CancellationToken cancellationToken = default);
    }
}
