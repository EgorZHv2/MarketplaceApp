using Data.DTO;
using Data.Entities;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentMethodController : BaseDictionaryController<PaymentMethod,PaymentMethodDTO>
    {
        public PaymentMethodController(IBaseDictionaryService<PaymentMethod, PaymentMethodDTO> dictionaryService)
        {
            _dictionaryService= dictionaryService;
        }
    }
}
