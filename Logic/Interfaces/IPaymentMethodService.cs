using Data.DTO;
using Data.Entities;

namespace Logic.Interfaces
{
    public interface IPaymentMethodService : IBaseDictionaryService<PaymentMethod, PaymentMethodDTO, PaymentMethodDTO>
    {
    }
}