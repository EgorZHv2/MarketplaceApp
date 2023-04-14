using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class PaymentMethodRepository : BaseDictionaryRepository<PaymentMethodEntity>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ApplicationDbContext context, IUserData userData) : base(context, userData)
        {
        }
    }
}