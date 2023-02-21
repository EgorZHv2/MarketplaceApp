using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Repositories
{
    public class ProductRepository:BaseRepository<ProductEntity>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext context):base(context) { }
    }
}
