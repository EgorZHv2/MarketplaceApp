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
    public class UserRepository:BaseRepository<User>,IUserRepository
    {


        public UserRepository(ApplicationDbContext context) : base(context) { }
       
     
        public async Task<User> GetUserByEmail(string email,CancellationToken cancellationToken = default)
        {
            
            return await _dbset.FirstOrDefaultAsync(e => e.Email == email,cancellationToken);
        }
       

      
    }
}
