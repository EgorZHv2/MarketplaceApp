﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class PaymentMethodNotFoundException:ApiException
    {
        public PaymentMethodNotFoundException():base("Тип оплаты не найден","Payment method not found",System.Net.HttpStatusCode.NotFound) { }
    }
}
