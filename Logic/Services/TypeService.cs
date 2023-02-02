using AutoMapper;
using Data.DTO;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class TypeService : BaseDictionaryService<Data.Entities.Type, TypeDTO, TypeDTO>, ITypeService
    {
        public TypeService(IRepositoryWrapper repositoryWrapper, IBaseDictionaryRepository<Data.Entities.Type> repository, IMapper mapper)
            : base(repositoryWrapper, repository, mapper) { }
    }
}