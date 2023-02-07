using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.DeliveryType
{
    public class UpdateDeliveryTypeDTO : BaseDictionaryUpdateDTO
    {
        public bool CanBeFree { get; set; }
    }
}