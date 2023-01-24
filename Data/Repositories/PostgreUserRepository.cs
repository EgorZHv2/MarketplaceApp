using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreUserRepository:BaseRepository<User>,IUserRepository
    {


        public PostgreUserRepository(ApplicationDbContext context) : base(context) { }
       
     
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(e => e.Email == email);
            return user;
        }
        public async Task<IQueryable<Shop>> GetFavoriteShopsByUserId(Guid userId)
        {
            var result =  _context.Set<User>().Include(e=>e.FavoriteShops).FirstOrDefaultAsync(e=>e.Id == userId).Result.FavoriteShops.AsQueryable();
            return result;
        }

      
    }
}
