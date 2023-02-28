using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IFileService
    {
        Task Upload(string directory,string filename,Stream datastream);
        Task Delete(string filepath);
        Task UploadFromWeb(string directory, string filename, string webPath);
    }
}
