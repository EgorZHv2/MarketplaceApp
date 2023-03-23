using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IFileHttpService:IBaseHttpService
    {
        Task<FileInfoDTO> PostAsync(CreateFileDTO dto);
        Task<FileInfoDTO> PostFileFromLinkAsync(CreateFileFromLinkDTO dto);
    }
}
