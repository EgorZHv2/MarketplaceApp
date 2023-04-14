using System.ComponentModel.DataAnnotations;

namespace Data.DTO.BaseDTOs.BaseDictionaryDTOs
{
    public class BaseDictionaryCreateDTO : BaseCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}