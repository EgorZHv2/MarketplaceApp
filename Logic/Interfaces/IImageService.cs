using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(IFormFile file, Guid entityid,CancellationToken cancellationToken = default);
    }
}
