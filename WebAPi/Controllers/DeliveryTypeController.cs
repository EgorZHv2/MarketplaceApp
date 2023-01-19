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
    public class DeliveryTypeController : BaseDictionaryController<DeliveryType,DeliveryTypeDTO>
    {
        public DeliveryTypeController(IBaseDictionaryService<DeliveryType, DeliveryTypeDTO> dictionaryService)
        {
            _dictionaryService= dictionaryService;
        }
    }
}
