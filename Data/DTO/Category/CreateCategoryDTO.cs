using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.Category
{
    public class CreateCategoryDTO : BaseDictionaryCreateDTO
    {
        public Guid? ParentCategoryId { get; set; }
    }
}