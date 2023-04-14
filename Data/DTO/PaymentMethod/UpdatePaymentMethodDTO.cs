using Data.DTO.BaseDTOs.BaseDictionaryDTOs;

namespace Data.DTO.PaymentMethod
{
    public class UpdatePaymentMethodDTO : BaseDictionaryUpdateDTO
    {
        public bool HasCommission { get; set; }
    }
}