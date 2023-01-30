using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Review
{
    public class UpdateReviewDTO:UpdateDTO
    {

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
