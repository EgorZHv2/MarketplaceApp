using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class DeliveryTypeRepository : BaseDictionaryRepository<DeliveryType>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}