using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PaymentMethod : BaseDictionaryEntity
    {
        public bool HasCommission { get; set; }
        public List<Shop> Shops { get; set; }= new List<Shop>();
        public List<ShopPaymentMethod> ShopPaymentMethods { get; set; } = new List<ShopPaymentMethod>();
    }
}
