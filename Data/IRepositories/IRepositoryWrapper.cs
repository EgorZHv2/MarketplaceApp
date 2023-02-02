using Data.Entities;
using Data.Repositories.DictionaryRepositories;

namespace Data.IRepositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository Users { get; }
        IShopRepository Shops { get; }
        IReviewRepository Reviews { get; }
        IStaticFileInfoRepository StaticFileInfos { get; }

        BaseDictionaryRepository<Category> Categories { get; }
        BaseDictionaryRepository<Data.Entities.Type> Types { get; }
        BaseDictionaryRepository<DeliveryType> DeliveryTypes { get; }
        BaseDictionaryRepository<PaymentMethod> PaymentMethods { get; }

        void Save();
    }
}