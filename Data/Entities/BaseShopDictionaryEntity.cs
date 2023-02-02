using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class BaseShopDictionaryEntity
    {
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
