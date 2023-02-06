using Data.DTO;
using Data.DTO.DeliveryType;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IDeliveryTypeService : IBaseDictionaryService<DeliveryType, DeliveryTypeDTO, CreateDeliveryTypeDTO,UpdateDeliveryTypeDTO,IDeliveryTypeRepository>
    {
    }
}