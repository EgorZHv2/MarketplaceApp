using Data.DTO.ProductXmlModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using Logic.Interfaces;
using System.Runtime.Serialization;
using Logic.Exceptions;

namespace Logic.Services
{
    public class XMLService:IXMLService
    {
        public T Deserialize<T>(IFormFile file) where T : class
        {
            T result;
            var settings = new XmlReaderSettings();
            settings.ProhibitDtd = false;

            using (var reader = XmlReader.Create(file.OpenReadStream(), settings))
            {
                try
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    result = xmlSerializer.Deserialize(reader) as T;
                }
                catch
                {
                    throw new XmlSerializationException();
                }
            }
            return result;
        }
    }
}
