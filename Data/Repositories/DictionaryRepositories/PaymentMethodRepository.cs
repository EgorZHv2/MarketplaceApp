using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.DictionaryRepositories
{
    public class PaymentMethodRepository:BaseDictionaryRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ApplicationDbContext context):base(context) { }
    }
}
