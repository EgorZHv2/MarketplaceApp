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
       
     
        public async Task<User> GetUserByEmail(string email,CancellationToken cancellationToken = default)
        {
            
            return await _dbset.FirstOrDefaultAsync(e => e.Email == email,cancellationToken);
        }
       

      
    }
}
