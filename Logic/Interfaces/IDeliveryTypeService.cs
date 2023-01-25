using Data.DTO;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IDeliveryTypeService:IBaseDictionaryService<DeliveryType,CreateDeliveryTypeDTO,CreateDeliveryTypeDTO>
    {
    }
}
