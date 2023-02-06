using Data.DTO;
using Data.DTO.PaymentMethod;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentMethodController : BaseDictionaryController<PaymentMethod, PaymentMethodDTO, CreatePaymentMethodDTO, UpdatePaymentMethodDTO, IPaymentMethodRepository, IPaymentMethodService>
    {
        public PaymentMethodController(IPaymentMethodService paymentMethodService) : base(paymentMethodService)
        {
        }
    }
}