using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Category
{
    public class CreateCategoryDTO:BaseDictionaryCreateDTO
    {
         public Guid? ParentCategoryId { get; set; }
    }
}
