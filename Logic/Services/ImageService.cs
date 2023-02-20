using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileservice;
        private readonly IStaticFileInfoRepository _staticFileInfo;

        private string _basepath => _configuration.GetSection("BaseImagePath").Value;
        private string[] _allowedImageExtensions => _configuration.GetSection("AllowedImageExtensions").Get<string[]>();

        public ImageService(IConfiguration configuration,
           IFileService fileService,
            IStaticFileInfoRepository staticFileInfo)
        {
            _configuration = configuration;
            _fileservice = fileService;
            _staticFileInfo = staticFileInfo;
        }

        public async Task CreateImage(Guid userId, IFormFile file, Guid entityId)
        {
            string fileextension = file.FileName.Split(".").Last();
            if (!_allowedImageExtensions.Contains(fileextension))
            {
                throw new WrongExtensionException("Картинка может быть только а png, jpg или jpeg", "Wrong image extension");
            }

            string filename = Guid.NewGuid().ToString();
            string foldername = _basepath + entityId.ToString();          
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
            var basefilepath = _basepath + id.ToString() + "/";

            foreach (var info in infos)
            {
                var filepath = basefilepath + info.Name + "." + info.Extension;
                await _fileservice.Delete(filepath);
            }
            await _staticFileInfo.HardDeleteMany( infos.ToArray());
        }
    }
}