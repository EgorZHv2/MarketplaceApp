using Data.DTO.Type;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface ITypeService : IBaseDictionaryService<TypeEntity, TypeDTO, CreateTypeDTO, UpdateTypeDTO, ITypeRepository>
    {
    }
}