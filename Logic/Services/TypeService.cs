using AutoMapper;
using Data.DTO.Type;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class TypeService : BaseDictionaryService<TypeEntity, TypeDTO, CreateTypeDTO, UpdateTypeDTO, ITypeRepository>, ITypeService
    {
        public TypeService(ITypeRepository repository, IMapper mapper)
            : base(repository, mapper) { }
    }
}