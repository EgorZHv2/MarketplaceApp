using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class FIleService:IFileService
    {
        public async Task Upload(string directory,string fileName,Stream datastream)
        {
            CheckDirectoryAndCreateIfNotExist(directory);
            
            var filePath = directory + "/" + fileName;
            using(var filestream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                await datastream.CopyToAsync(filestream);
            }
        }
        public Task Delete(string filePath)
        {
            
            if (!File.Exists(filePath))
            {
                throw new DirectoryNotFoundException("Папка не найдена");
            }
            File.Delete(filePath);
            return Task.CompletedTask;
        }
      

        private void CheckDirectoryAndCreateIfNotExist(string directory)
        {
            var fullPath = Path.Combine(directory);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }
       
        
    }
}
