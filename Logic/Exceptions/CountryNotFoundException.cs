﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class CountryNotFoundException:ApiException
    {
        public CountryNotFoundException():base("Country not found",System.Net.HttpStatusCode.NotFound) { }
    }
}
