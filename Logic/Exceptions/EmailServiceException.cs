﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class EmailServiceException:ApiException
    {
        public EmailServiceException(string userMessage, string logMessage) : base(userMessage, logMessage, HttpStatusCode.NotFound)
        {
        }
    }
}