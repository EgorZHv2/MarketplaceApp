using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopDeliveryTypes
    {
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public decimal FreeDeliveryThreshold { get; set; }
    }
}
