using System.ComponentModel.DataAnnotations;

namespace Data.DTO.BaseDTOs
{
    public class BaseUpdateDTO 
    {
        [Required]
        public Guid Id { get; set; }
    }
}