using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopPaymentMethod:BaseShopDictionaryEntity
    {
     
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double? Сommission { get; set; }
        
    }
}
