using Data.DTO;
using Data.DTO.Type;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface ITypeService : IBaseDictionaryService<Data.Entities.Type, TypeDTO, CreateTypeDTO,UpdateTypeDTO,ITypeRepository>
    {
    }
}