using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public Guid? UpdatedBy { get; set; }

        public DateTime? DeleteDateTime { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsActive { get; set; }
      
    }
}