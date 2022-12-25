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

        private IUserRepository _user;
        private IShopRepository _shop;
        private IReviewRepository _review;

        public IUserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new PostgreUserRepository(_context);
                }
                return _user;
            }
        }
        public IShopRepository Shop
        {
            get
            {
                if(_shop == null)
                {
                    _shop = new PostgreShopRepository(_context);
                }
                return _shop;
            }
        }

        public IReviewRepository Review
        {
            get
            {
                if(_review == null)
                {
                    _review = new PostgreReviewRepository(_context);
                }
                return _review;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
