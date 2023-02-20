using AutoMapper;
using Data.DTO.DeliveryType;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class DeliveryTypeService : BaseDictionaryService<DeliveryTypeEntity, DeliveryTypeDTO, CreateDeliveryTypeDTO, UpdateDeliveryTypeDTO, IDeliveryTypeRepository>, IDeliveryTypeService
    {
        public DeliveryTypeService(IDeliveryTypeRepository repository, IMapper mapper)
            : base(repository, mapper) { }
    }
}