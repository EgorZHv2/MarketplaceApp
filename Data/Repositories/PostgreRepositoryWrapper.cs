using Data.IRepositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Data.Repositories
{
    public class PostgreRepositoryWrapper:IRepositoryWrapper
    {
        private ApplicationDbContext _context;

        private IUserRepository _users;
        private IShopRepository _shops;
        private IReviewRepository _reviews;

        public PostgreRepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public IUserRepository Users
        {
            get
            {
                if(_users == null)
                {
                    _users = new PostgreUserRepository(_context);
                }
                return _users;
            }
        }
        public IShopRepository Shops
        {
            get
            {
                if(_shops == null)
                {
                    _shops = new PostgreShopRepository(_context);
                }
                return _shops;
            }
        }

        public IReviewRepository Reviews
        {
            get
            {
                if(_reviews == null)
                {
                    _reviews = new PostgreReviewRepository(_context);
                }
                return _reviews;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
