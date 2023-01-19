using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IRepositoryWrapper
    {
         IUserRepository Users { get; }
        IShopRepository Shops { get; }
        IReviewRepository Reviews { get; }
        IUsersFavShopsRepository UsersFavShops { get; }
        IStaticFileInfoRepository StaticFileInfos { get; }

        BaseDictionaryRepository<Category> Categories { get; } 
       BaseDictionaryRepository<Data.Entities.Type> Types { get; } 
        BaseDictionaryRepository<DeliveryType> DeliveryTypes { get; }
       BaseDictionaryRepository<PaymentMethod> PaymentMethods { get; }
        void Save();
    }
}
