using Data.DTO;
using Data.DTO.DeliveryType;
using Data.DTO.PaymentMethod;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IPaymentMethodService : IBaseDictionaryService<PaymentMethod, PaymentMethodDTO, CreatePaymentMethodDTO, UpdatePaymentMethodDTO, IPaymentMethodRepository>
    {
    }
}