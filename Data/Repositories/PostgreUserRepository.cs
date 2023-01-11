using Data.Entities;
using Data.IRepositories;
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
       
     
        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email);
            return user;
        }
    }
}
