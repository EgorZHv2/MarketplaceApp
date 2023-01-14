using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreUsersFavShopsRepository:BaseRepository<UsersFavShops>,IUsersFavShopsRepository
    {
        public PostgreUsersFavShopsRepository(ApplicationDbContext context):base(context.UsersFavShops) { }
    }
}
