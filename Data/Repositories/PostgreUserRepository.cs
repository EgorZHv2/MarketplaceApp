using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreUserRepository:IRepository<User>,IUserRepository
    {
          private ApplicationDbContext _context;

        public PostgreUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        public User GetById(Guid Id)
        {
            return _context.Users.Find(Id);
        }
        public void Create(User user)
        {
            _context.Users.Add(user);
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
        }
        public void Delete(Guid Id)
        {
            var entity = _context.Users.Find(Id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
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
