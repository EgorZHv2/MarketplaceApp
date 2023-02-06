using AutoMapper;
using Data.DTO;
using Data.DTO.DeliveryType;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;

namespace Logic.Services
{
    public class DeliveryTypeService : BaseDictionaryService<DeliveryType, DeliveryTypeDTO, CreateDeliveryTypeDTO,UpdateDeliveryTypeDTO,IDeliveryTypeRepository>, IDeliveryTypeService
    {
        public DeliveryTypeService(IDeliveryTypeRepository repository, IMapper mapper)
            : base( repository, mapper) { }
    }
}