using Data.DTO;
using Data.Entities;
using Data.Models;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IBaseDictionaryService<TEntity,TCreateDTO,TDTO> where TEntity: BaseDictionaryEntity where TDTO: DictionaryDTO
    {
        public  Task Create(Guid userid, TCreateDTO model);

        public  Task Update(Guid userid, TCreateDTO model);

        public  Task<PageModel<TDTO>> GetPage(FilterPagingModel model);

        public  Task Delete(Guid userid, Guid Id);
       
    }
}
