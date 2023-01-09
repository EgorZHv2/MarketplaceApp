using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Models
{
    public class ReviewModel
    {
     
        public Guid Id { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public Guid BuyerId { get; set; }
        [Required]
        public Guid ShopId { get; set; }
       
    }
}
