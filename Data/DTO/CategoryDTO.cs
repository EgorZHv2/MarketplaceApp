namespace Data.DTO
{
    public class CategoryDTO : DictionaryDTO
    {
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}