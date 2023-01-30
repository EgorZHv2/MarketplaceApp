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
    }
}
