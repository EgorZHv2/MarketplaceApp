using System.ComponentModel.DataAnnotations;

namespace Data.DTO
{
    public class BaseUpdateDTO : BaseDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}