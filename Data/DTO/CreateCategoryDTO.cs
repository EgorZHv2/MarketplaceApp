namespace Data.DTO
{
    public class CreateCategoryDTO : DictionaryDTO
    {
        public Guid? ParentCategoryId { get; set; }
    }
}