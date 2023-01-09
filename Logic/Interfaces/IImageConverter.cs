using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Interfaces
{
    public interface IImageConverter
    {
         byte[] ConvertFormFileToByte(IFormFile image);
    }
}
