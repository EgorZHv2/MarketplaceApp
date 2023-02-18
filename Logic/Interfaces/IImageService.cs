using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(Guid userId, IFormFile file, Guid entityId);

        Task DeleteAllImagesByParentId(Guid userId, Guid id);
    }
}