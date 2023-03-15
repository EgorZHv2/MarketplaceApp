using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class RequiredImportPropertyException:ApiException
    {
        public string RequiredPropertyName { get; set; }
        public RequiredImportPropertyException(string propertyName) : base($"Required property {propertyName} was null", System.Net.HttpStatusCode.BadRequest) 
        {
            RequiredPropertyName = propertyName;
        }
    }
}
