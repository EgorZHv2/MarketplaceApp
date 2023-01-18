using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StaticFileInfo:BaseEntity
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public Guid ParentEntityId { get; set; }
    }
}
