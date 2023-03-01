using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IFileService
    {
        Task Upload(string directory,string fileName,Stream datastream);
        Task Delete(string filePath);
        Task UploadFromWeb(string directory, string fileName, string webPath);
    }
}
