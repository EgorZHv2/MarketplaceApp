using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Review
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ShopId { get; set; }
    }
}
