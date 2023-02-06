using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Category
{
    public class CategoryDTO:BaseDictinoaryOutputDTO
    {
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    }
}
