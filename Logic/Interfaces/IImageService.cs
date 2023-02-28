using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(Guid userId, IFormFile file, Guid entityId);
        Task CreateImage(Guid userId, string webPath, Guid entityId);
        Task CreateManyImages(Guid userId, ICollection<IFormFile> files, Guid entityId);
        Task CreateManyImages(Guid userId, ICollection<string> webPaths, Guid entityId);

        Task DeleteAllImagesByParentId(Guid userId, Guid id);
    }
}