using Data.DTO;
using Data.Entities;
using Data.Models;
using Data.DTO.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IShopService:IBaseService<Shop,ShopDTO,CreateShopDTO,UpdateShopDTO,IShopRepository>
    {
        public Task<List<ShopDTO>> GetAll(Guid userid, CancellationToken cancellationToken = default);
        public  Task AddShopToFavorites(Guid userId, Guid shopId,CancellationToken cancellationToken = default);
        public Task<List<ShopDTO>> ShowUserFavoriteShops(Guid userId, CancellationToken cancellationToken = default);
        public Task DeleteShopFromFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);
    }
}
