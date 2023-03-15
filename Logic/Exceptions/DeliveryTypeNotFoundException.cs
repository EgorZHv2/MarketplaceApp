﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class DeliveryTypeNotFoundException:ApiException
    {
        public DeliveryTypeNotFoundException() : base("Delivery type not found", System.Net.HttpStatusCode.NotFound) { }
    }
}
