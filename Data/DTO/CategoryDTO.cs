using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class CategoryDTO:DictionaryDTO
    {
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
