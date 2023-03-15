using Logic.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Exceptions;
using Dadata.Model;

namespace Logic.Helpers
{
    public class LocalizationHelper
    {
        public static IStringLocalizer StringLocalizer { get; set; }
        
       
        public static string GetString(string key)
        {
           
            return  StringLocalizer[key].Value;;
            
        }
    }
}
