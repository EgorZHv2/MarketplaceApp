using Data.DTO;
using Data.Entities;

namespace Logic.Interfaces
{
    public interface IDeliveryTypeService : IBaseDictionaryService<DeliveryType, CreateDeliveryTypeDTO, CreateDeliveryTypeDTO>
    {
    }
}