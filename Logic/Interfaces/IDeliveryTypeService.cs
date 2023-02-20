using Data.DTO.DeliveryType;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IDeliveryTypeService : IBaseDictionaryService<DeliveryTypeEntity, DeliveryTypeDTO, CreateDeliveryTypeDTO, UpdateDeliveryTypeDTO, IDeliveryTypeRepository>
    {
    }
}