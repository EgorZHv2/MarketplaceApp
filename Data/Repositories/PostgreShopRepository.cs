using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreShopRepository : BaseRepository<Shop>,IShopRepository
    {       
       public  PostgreShopRepository(ApplicationDbContext context):base(context) { }
    }
}
