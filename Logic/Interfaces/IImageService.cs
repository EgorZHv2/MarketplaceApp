using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IImageService
    {
        Task<Guid> CreateImage(IFormFile file, Guid entityId);
        Task<Guid> CreateImage(string webPath, Guid entityId);
        Task<List<Guid>> CreateManyImages(ICollection<IFormFile> files, Guid entityId);
        Task<List<Guid>> CreateManyImages(ICollection<string> webPaths, Guid entityId);

        Task DeleteAllImagesByParentId(Guid id);
    }
}