using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(IFormFile file, Guid entityid, CancellationToken cancellationToken = default);
    }
}