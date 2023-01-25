using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class DeliveryType : BaseDictionaryEntity
    {
        public bool CanBeFree { get; set; }
        public List<Shop> Shops { get; set; }= new List<Shop>();
    }
}
