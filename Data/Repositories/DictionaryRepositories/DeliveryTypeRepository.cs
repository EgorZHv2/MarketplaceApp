﻿using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class DeliveryTypeRepository : BaseDictionaryRepository<DeliveryTypeEntity>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}