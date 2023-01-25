using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Shop:BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public List<Review> Reviews { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<DeliveryType> DeliveryTypes { get; set; } = new List<DeliveryType>();
        public List<Type> Types { get; set; } = new List<Type>();
        public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public List<ShopDeliveryTypes> ShopDeliveryTypes { get; set; } = new List<ShopDeliveryTypes>();
        public List<ShopPaymentMethod> ShopPaymentMethods { get; set; } = new List<ShopPaymentMethod>();
    }
}
