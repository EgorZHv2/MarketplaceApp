using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid UpdatorId { get; set; }
        public DateTime DeleteDateTime { get; set; }
        public Guid DeletorId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
