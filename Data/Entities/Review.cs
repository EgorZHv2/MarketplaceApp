using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
