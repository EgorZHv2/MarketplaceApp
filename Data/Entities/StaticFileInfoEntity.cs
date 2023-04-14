namespace Data.Entities
{
    public class StaticFileInfoEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public Guid ParentEntityId { get; set; }
    }
}