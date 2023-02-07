using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(Guid userId, IFormFile file, Guid entityid, CancellationToken cancellationToken = default);

        Task DeleteAllImagesByParentId(Guid userId, Guid id, CancellationToken cancellationToken = default);
    }
}