using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context) 
        {
           context.Database.EnsureDeleted();
           context.Database.EnsureCreated();       
        }
    }
}
