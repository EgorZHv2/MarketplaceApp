using AutoMapper;
using Data.DTO.PaymentMethod;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class PaymentMethodService : BaseDictionaryService<PaymentMethodEntity, PaymentMethodDTO, CreatePaymentMethodDTO, UpdatePaymentMethodDTO, IPaymentMethodRepository>, IPaymentMethodService
    {
        public PaymentMethodService(IPaymentMethodRepository repository, IMapper mapper)
           : base(repository, mapper) { }
    }
}