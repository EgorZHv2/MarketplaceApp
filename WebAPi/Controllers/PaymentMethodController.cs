using Data.DTO;
using Data.Entities;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentMethodController : BaseDictionaryController<PaymentMethod, PaymentMethodDTO, PaymentMethodDTO>
    {
        public PaymentMethodController(IPaymentMethodService paymentMethodService) : base(paymentMethodService)
        {
        }
    }
}