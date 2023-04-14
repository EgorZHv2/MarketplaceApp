using System.ComponentModel.DataAnnotations;

namespace Data.DTO
{
    public class EntityActivityDTO
    {
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}