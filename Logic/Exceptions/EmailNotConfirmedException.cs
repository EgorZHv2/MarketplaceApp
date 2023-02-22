﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class EmailNotConfirmedException:ApiException
    {
        public EmailNotConfirmedException():base("Почта не подтверждена","Email not confirmed",System.Net.HttpStatusCode.Forbidden) { }
    }
}
