using System.ComponentModel.DataAnnotations;

namespace Data.DTO.BaseDTOs
{
    public class BaseUpdateDTO : BaseOutputDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}