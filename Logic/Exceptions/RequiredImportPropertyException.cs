using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class RequiredImportPropertyException:ApiException
    {
        public RequiredImportPropertyException(string propertyName) : base($"Нужно заполнить все обязательные поля. Поле {propertyName} было пустым", $"Required property {propertyName} was null", System.Net.HttpStatusCode.BadRequest) { }
    }
}
