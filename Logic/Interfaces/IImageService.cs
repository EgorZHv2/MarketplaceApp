using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task CreateImage(IFormFile file, Guid entityId);
        Task CreateImage(string webPath, Guid entityId);
        Task CreateManyImages(ICollection<IFormFile> files, Guid entityId);
        Task CreateManyImages(ICollection<string> webPaths, Guid entityId);

        Task DeleteAllImagesByParentId(Guid id);
    }
}