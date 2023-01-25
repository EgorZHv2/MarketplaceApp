using Data.DTO;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ICategoryService:IBaseDictionaryService<Category,CreateCategoryDTO,CategoryDTO>
    {
        public Task<bool> CheckParentCategory(Guid categoryid,Guid parentid);
        public Task<List<CategoryDTO>> GetCategoryTree();
    }
}
