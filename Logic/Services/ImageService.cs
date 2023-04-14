using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Options;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationOptions _options;
        private readonly IFileService _fileservice;
        private readonly IStaticFileInfoRepository _staticFileInfo;
        private readonly IFileHttpService _fileHttpService;


        public ImageService(
            IOptions<ApplicationOptions> options,
            IFileService fileService,
            IStaticFileInfoRepository staticFileInfo,
            IFileHttpService fileHttpService
            
        )
        {
            _options = options.Value;
            _fileservice = fileService;
            _staticFileInfo = staticFileInfo;
            _fileHttpService = fileHttpService;
        }

        public async Task<Guid> CreateImage(string webPath, Guid entityId)
        {
            var fileExtension = webPath.Split("/").Last().Split(".").Last();
            if (!_options.AllowedImageExtensions.Contains(fileExtension))
            {
                throw new WrongExtensionException();
            }
            var fileName = Guid.NewGuid().ToString();
            var folderName = _options.BaseImagePath + entityId.ToString();
            var request = HttpWebRequest.Create(webPath);
            var response = request.GetResponse();
            await _fileservice.Upload(folderName,fileName + "." + fileExtension, response.GetResponseStream());
            
    
            var entity = new StaticFileInfoEntity
            {
                Extension = fileExtension,
                Name = fileName,
                ParentEntityId = entityId
            };
            var existing = await _staticFileInfo.GetAllByParentId(entityId);
            if (existing.Any())
            {
                await DeleteAllImagesByParentId(entityId);
            }
            return  await _staticFileInfo.Create(entity);
        }

        public async Task<Guid> CreateImage(IFormFile file, Guid entityId)
        {
            var fileExtension = file.FileName.Split(".").Last();
            if (!_options.AllowedImageExtensions.Contains(fileExtension))
            {
                throw new WrongExtensionException();
            }
            //await _httpService.PostAsync<FileInfoDTO>(_options.FileControllerPath, new CreateFileDTO { EntityId = entityId, File = file });
            var fileName = Guid.NewGuid().ToString();
            var folderName = _options.BaseImagePath + entityId.ToString();
            await _fileservice.Upload(
                folderName,
                fileName + "." + fileExtension,
                file.OpenReadStream()
            );

            var entity = new StaticFileInfoEntity
            {
                Extension = fileExtension,
                Name = fileName,
                ParentEntityId = entityId
            };
            var existing = await _staticFileInfo.GetAllByParentId(entityId);

            if (existing.Any())
            {
                await DeleteAllImagesByParentId(entityId);
            }

            return  await _staticFileInfo.Create(entity);
        }

        public async Task<List<Guid>> CreateManyImages(ICollection<IFormFile> files, Guid entityId)
        {
            var existing = await _staticFileInfo.GetAllByParentId(entityId);
            if (existing.Any())
            {
                await DeleteAllImagesByParentId(entityId);
            }
            var result = new List<Guid>();
            foreach (var file in files)
            {
                var fileExtension = file.FileName.Split(".").Last();
                if (!_options.AllowedImageExtensions.Contains(fileExtension))
                {
                    throw new WrongExtensionException();
                }

                var fileName = Guid.NewGuid().ToString();
                var folderName = _options.BaseImagePath + entityId.ToString();
                await _fileservice.Upload(
                    folderName,
                    fileName + "." + fileExtension,
                    file.OpenReadStream()
                );

                var entity = new StaticFileInfoEntity
                {
                    Extension = fileExtension,
                    Name = fileName,
                    ParentEntityId = entityId
                };
                result.Add(await _staticFileInfo.Create(entity));
            }
            return result;
        }

        public async Task<List<Guid>> CreateManyImages(ICollection<string> webPaths, Guid entityId)
        {
            var existing = await _staticFileInfo.GetAllByParentId(entityId);
            if (existing.Any())
            {
                await DeleteAllImagesByParentId(entityId);
            }
            var result = new List<Guid>();
            foreach (var webPath in webPaths)
            {
                var fileExtension = webPath.Split("/").Last().Split(".").Last();
                if (!_options.AllowedImageExtensions.Contains(fileExtension))
                {
                    throw new WrongExtensionException();
                }
                var fileName = Guid.NewGuid().ToString(); ;
                var folderName = _options.BaseImagePath + entityId.ToString();
                var request = HttpWebRequest.Create(webPath);
                var response = request.GetResponse();
                await _fileservice.Upload(folderName, fileName + "." + fileExtension, response.GetResponseStream());
                StaticFileInfoEntity entity = new StaticFileInfoEntity
                {
                    Extension = fileExtension,
                    Name = fileName,
                    ParentEntityId = entityId
                };
                result.Add(await _staticFileInfo.Create(entity));
            }
            return result;
        }

        public async Task DeleteAllImagesByParentId(Guid id)
        {
            var infos = await _staticFileInfo.GetAllByParentId(id);
            var baseFilePath = _options.BaseImagePath + id.ToString() + "/";

            foreach (var info in infos)
            {
                var filePath = baseFilePath + info.Name + "." + info.Extension;
                await _fileservice.Delete(filePath);
            }
            await _staticFileInfo.HardDeleteMany(infos.ToArray());
        }
    }
}
