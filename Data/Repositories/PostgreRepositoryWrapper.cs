using Data.Entities;
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
        private IUsersFavShopsRepository _usersFavShops;
        private IStaticFileInfoRepository _staticFileInfos;
        private BaseDictionaryRepository<Category> _categories;
        private BaseDictionaryRepository<Data.Entities.Type> _types;
        private BaseDictionaryRepository<DeliveryType> _deliveryTypes;
        private BaseDictionaryRepository<PaymentMethod> _paymentMethods;
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
        public IUsersFavShopsRepository UsersFavShops
        {
            get
            {
                if(_usersFavShops == null)
                {
                    _usersFavShops = new PostgreUsersFavShopsRepository(_context);
                }
                return _usersFavShops;
            }
        }
        public IStaticFileInfoRepository StaticFileInfos
        {
            get
            {
                if (_staticFileInfos == null)
                {
                    _staticFileInfos = new PostgreStaticFileInfoRepository(_context);
                }
                return _staticFileInfos;
            }
        }
        public BaseDictionaryRepository<Category> Categories => _categories ??= new BaseDictionaryRepository<Category>(_context.Categories);
        public BaseDictionaryRepository<Data.Entities.Type> Types => _types ??= new BaseDictionaryRepository<Data.Entities.Type>(_context.Types);
        public BaseDictionaryRepository<DeliveryType> DeliveryTypes => _deliveryTypes ??= new BaseDictionaryRepository<DeliveryType>(_context.DeliveryTypes);
        public BaseDictionaryRepository<PaymentMethod> PaymentMethods => _paymentMethods ??= new BaseDictionaryRepository<PaymentMethod>(_context.PaymentMethods);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
