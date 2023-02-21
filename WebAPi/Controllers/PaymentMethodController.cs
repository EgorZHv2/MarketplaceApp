using Data.DTO.PaymentMethod;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : BaseDictionaryController<PaymentMethodEntity, PaymentMethodDTO, CreatePaymentMethodDTO, UpdatePaymentMethodDTO, IPaymentMethodRepository, IPaymentMethodService>
    {
        public PaymentMethodController(IPaymentMethodService paymentMethodService) : base(paymentMethodService)
        {
        }
    }
}