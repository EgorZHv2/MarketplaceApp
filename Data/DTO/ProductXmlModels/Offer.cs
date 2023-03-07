using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.DTO
{
    [Serializable]

    public class Offer
    {
        
        public decimal price { get; set; }
        
        public int vendorCode { get;set; }
       
        public double count { get; set; }
       
        public string description { get; set; }
    }
}
