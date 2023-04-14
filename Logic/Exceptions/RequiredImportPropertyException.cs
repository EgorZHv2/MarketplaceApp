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
        public int Row { get; set; }
        public int Column { get; set; }
        public RequiredImportPropertyException(string propertyName, int row,int column) : base($"Required property {propertyName} was null", System.Net.HttpStatusCode.BadRequest) 
        {
            RequiredPropertyName = propertyName;
            Row = row;
            Column = column;
        }
    }
}
