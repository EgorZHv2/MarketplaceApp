using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class FIleService:IFileService
    {
        public async Task Upload(string directory,string filename,Stream datastream)
        {
            CheckDirectoryAndCreateIfNotExist(directory);
            
            var filepath = directory + "/" + filename;
            using(var filestream = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                await datastream.CopyToAsync(filestream);
            }
        }
        public Task Delete(string filepath)
        {
            
            if (!File.Exists(filepath))
            {
                throw new DirectoryNotFoundException("Папка не найдена");
            }
            File.Delete(filepath);
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
        private string GenerateNewFileName(string fileextension) => $"{Guid.NewGuid()}.{fileextension}";
        
    }
}
