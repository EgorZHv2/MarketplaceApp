using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.Category
{
    public class UpdateCategoryDTO : BaseDictionaryUpdateDTO
    {
        public Guid? ParentCategoryId { get; set; }
    }
}