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
    public class ShopNode
    {
        [XmlArray("offers")]
        [XmlArrayItem("offer")]
        public List<OfferNode> offers { get; set; } = new List<OfferNode>();
        public int Num { get; set; }
    }
}
