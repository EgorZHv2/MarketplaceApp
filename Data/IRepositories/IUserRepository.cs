using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public Task<User> GetUserByEmail(string email);

        public Task<IQueryable<Shop>> GetFavoriteShopsByUserId(Guid userId);
    }
}
