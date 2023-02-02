using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IBaseShopDictionaryRepository<TEntity>
    {
        public Task CreateRange(CancellationToken cancellationToken = default, params TEntity[] entities);

        public Task DeleteAllByShop(Shop shop, CancellationToken cancellationToken = default);
       
    }
}
