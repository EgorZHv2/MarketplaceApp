using Data.Entities;
using Data.IRepositories;
using Data.Options;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationOptions _options;
        private readonly IFileService _fileservice;
        private readonly IStaticFileInfoRepository _staticFileInfo;

        
        

        public ImageService(IOptions<ApplicationOptions> options,
           IFileService fileService,
            IStaticFileInfoRepository staticFileInfo)
        {
            _options = options.Value;
            _fileservice = fileService;
            _staticFileInfo = staticFileInfo;
        }

        public async Task CreateImage(Guid userId, IFormFile file, Guid entityId)
        {
            string fileextension = file.FileName.Split(".").Last();
            if (!_options.AllowedImageExtensions.Contains(fileextension))
            {
                throw new WrongExtensionException();
            }

            string filename = Guid.NewGuid().ToString();
            string foldername = _options.BaseImagePath + entityId.ToString();          
            await _fileservice.Upload(foldername,filename + "." + fileextension, file.OpenReadStream());
    
            StaticFileInfoEntity entity = new StaticFileInfoEntity
            {
                Extension = fileextension,
                Name = filename,
                ParentEntityId = entityId
            };
            var existing = await _staticFileInfo.GetAllByParentId(entityId);

            if (existing.Any())
            {
                await DeleteAllImagesByParentId(userId, entityId);
            }

            await _staticFileInfo.Create(userId, entity);
        }

        public async Task DeleteAllImagesByParentId(Guid userId, Guid id)
        {
            var infos = await _staticFileInfo.GetAllByParentId(id);
            var basefilepath = _options.BaseImagePath + id.ToString() + "/";

            foreach (var info in infos)
            {
                var filepath = basefilepath + info.Name + "." + info.Extension;
                await _fileservice.Delete(filepath);
            }
            await _staticFileInfo.HardDeleteMany( infos.ToArray());
        }
    }
}