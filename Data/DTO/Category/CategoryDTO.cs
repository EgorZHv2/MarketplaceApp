using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.Category
{
    public class CategoryDTO : BaseDictinoaryOutputDTO
    {
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    }
}