using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.DictionaryRepositories
{
    public class DeliveryTypeRepository:BaseDictionaryRepository<DeliveryType>,IDeliveryTypeRepository
    {
          public DeliveryTypeRepository(ApplicationDbContext context):base(context) { }
    }
}
