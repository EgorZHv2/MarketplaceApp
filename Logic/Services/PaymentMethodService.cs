using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class PaymentMethodService : BaseDictionaryService<PaymentMethod, PaymentMethodDTO, PaymentMethodDTO>, IPaymentMethodService
    {
        public PaymentMethodService(IRepositoryWrapper repositoryWrapper, IBaseDictionaryRepository<PaymentMethod> repository, IMapper mapper)
           : base(repositoryWrapper, repository, mapper) { }
    }
}