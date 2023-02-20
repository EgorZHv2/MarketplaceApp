using Data.DTO.DeliveryType;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeliveryTypeController : BaseDictionaryController<DeliveryTypeEntity, DeliveryTypeDTO, CreateDeliveryTypeDTO, UpdateDeliveryTypeDTO, IDeliveryTypeRepository, IDeliveryTypeService>
    {
        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService) : base(deliveryTypeService)
        {
        }
    }
}