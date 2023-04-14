using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Type
{
    public class UpdateTypeDTO : BaseDictionaryUpdateDTO
    {
        [Required]
        [MaxLength(500, ErrorMessage = "Поле «{0}» превысило максимально допустимое значение в «{1}» символов")]
        public string Description { get; set; }
    }
}