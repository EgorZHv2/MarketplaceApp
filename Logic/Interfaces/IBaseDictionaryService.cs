using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IBaseDictionaryService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository> : IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        where TEntity : BaseDictionaryEntity
        where TRepository : IBaseDictionaryRepository<TEntity>
        where TUpdateDTO : BaseDictionaryUpdateDTO
        where TDTO : BaseDictinoaryOutputDTO
        where TCreateDTO : BaseDictionaryCreateDTO
    {
    }
}