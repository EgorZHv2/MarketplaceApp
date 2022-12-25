using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreShopRepository : IRepository<Shop>,IShopRepository
    {
        private ApplicationDbContext _context;

        public PostgreShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Shop> GetAll()
        {
            return _context.Shops;
        }
        public Shop GetById(Guid Id)
        {
            return _context.Shops.Find(Id);
        }
        public void Create(Shop shop)
        {
            _context.Shops.Add(shop);
        }
        public void Update(Shop shop)
        {
            _context.Shops.Update(shop);
        }
        public void Delete(Guid Id)
        {
            var entity = _context.Shops.Find(Id);
            if (entity != null)
            {
                _context.Shops.Remove(entity);
            }
        }
        
        
        private bool disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}
