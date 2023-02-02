using System.ComponentModel.DataAnnotations;

namespace Data.DTO
{
    public class TypeDTO : DictionaryDTO
    {
        [Required]
        [MaxLength(500, ErrorMessage = "Поле «{0}» превысило максимально допустимое значение в «{1}» символов")]
        public string Description { get; set; }
    }
}