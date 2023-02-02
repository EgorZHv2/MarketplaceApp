using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopCategory:BaseShopDictionaryEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
