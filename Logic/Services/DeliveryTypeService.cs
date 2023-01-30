﻿using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class DeliveryTypeService:BaseDictionaryService<DeliveryType,CreateDeliveryTypeDTO,CreateDeliveryTypeDTO>,IDeliveryTypeService
    {
        public DeliveryTypeService(IRepositoryWrapper repositoryWrapper,IBaseDictionaryRepository<DeliveryType> repository, IMapper mapper)
            : base(repositoryWrapper, repository, mapper) { }
    }
}