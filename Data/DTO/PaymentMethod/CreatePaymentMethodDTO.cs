using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.PaymentMethod
{
    public class CreatePaymentMethodDTO : BaseDictionaryCreateDTO
    {
        public bool HasCommission { get; set; }
    }
}