using AutoMapper;
using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BaseDictionaryService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        : BaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>,
          IBaseDictionaryService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        where TEntity : BaseDictionaryEntity
        where TRepository : IBaseDictionaryRepository<TEntity>
        where TUpdateDTO : BaseDictionaryUpdateDTO
        where TDTO : BaseDictinoaryOutputDTO
        where TCreateDTO : BaseDictionaryCreateDTO
    {
        public BaseDictionaryService(TRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}