using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopType:BaseShopDictionaryEntity
    {
      public Guid TypeId { get; set; }
      public Data.Entities.Type Type { get; set; }
    }
}
