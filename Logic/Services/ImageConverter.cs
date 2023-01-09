using WebAPi.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Services
{
    public class ImageConverter:IImageConverter
    {
        public byte[] ConvertFormFileToByte(IFormFile image)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)image.Length);
            }

            return imageData;
        }
    }
}
