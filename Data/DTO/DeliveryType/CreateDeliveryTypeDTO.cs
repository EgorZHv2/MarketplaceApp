﻿using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.DeliveryType
{
    public class CreateDeliveryTypeDTO:BaseDictionaryCreateDTO
    {
         public bool CanBeFree { get; set; }
    }
}
