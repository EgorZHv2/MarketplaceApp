using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopDeliveryType:BaseShopDictionaryEntity
    {
      
        public Guid DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public decimal? FreeDeliveryThreshold { get; set; }
    }
}
