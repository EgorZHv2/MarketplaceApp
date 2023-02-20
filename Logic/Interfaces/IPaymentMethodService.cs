using Data.DTO.PaymentMethod;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IPaymentMethodService : IBaseDictionaryService<PaymentMethodEntity, PaymentMethodDTO, CreatePaymentMethodDTO, UpdatePaymentMethodDTO, IPaymentMethodRepository>
    {
    }
}