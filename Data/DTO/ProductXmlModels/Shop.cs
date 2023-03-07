using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.DTO.ProductXmlModels
{
    [Serializable]
    [XmlRoot("shop")]
    [XmlType("shop")]
    public class Shop
    {
        [XmlArray("offers")]
        [XmlArrayItem("offer")]
        public List<Offer> offers { get; set; } = new List<Offer>();
        public int Num { get; set; }
    }
}
