using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreReviewRepository:IRepository<Review>,IReviewRepository
    {
         private ApplicationDbContext _context;

        public PostgreReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews;
        }
        public Review GetById(Guid Id)
        {
            return _context.Reviews.Find(Id);
        }
        public void Create(Review review)
        {
            _context.Reviews.Add(review);
        }
        public void Update(Review review)
        {
            _context.Reviews.Update(review);
        }
        public void Delete(Guid Id)
        {
            var entity = _context.Reviews.Find(Id);
            if (entity != null)
            {
                _context.Reviews.Remove(entity);
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
