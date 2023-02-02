using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class PaymentMethodRepository : BaseDictionaryRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}