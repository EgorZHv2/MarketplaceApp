using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.DeliveryType
{
    public class CreateDeliveryTypeDTO : BaseDictionaryCreateDTO
    {
        public bool CanBeFree { get; set; }
    }
}