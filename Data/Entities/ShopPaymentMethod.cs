using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopPaymentMethod
    {
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double Сommission { get; set; }
        
    }
}
