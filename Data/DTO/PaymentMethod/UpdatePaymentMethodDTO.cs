using Data.DTO.BaseDTOs.BaseDictionaryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.PaymentMethod
{
    public class UpdatePaymentMethodDTO:BaseDictionaryUpdateDTO
    {
         public bool HasCommission { get; set; }
    }
}
