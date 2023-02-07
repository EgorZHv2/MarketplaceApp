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
        private readonly ILogger<ImageService> _logger;
        private readonly IStaticFileInfoRepository _staticFileInfo;
        private string _basepath => _configuration.GetSection("BaseImagePath").Value;

        public ImageService(IConfiguration configuration,
            ILogger<ImageService> logger,
            IStaticFileInfoRepository staticFileInfo)
        {
            _configuration = configuration;
            _logger = logger;
            _staticFileInfo = staticFileInfo;
        }

        public async Task CreateImage(Guid userId, IFormFile file, Guid entityid, CancellationToken cancellationToken = default)
        {
            string fileextension = file.FileName.Split(".").Last();
            if (!(fileextension == "png" || fileextension == "jpg" || fileextension == "jpeg"))
            {
                throw new WrongExtensionException("Картинка может быть только а png, jpg или jpeg", "Wrong image extension");
            }
            string filename = Guid.NewGuid().ToString();
            string foldername = entityid.ToString();
            string filepath = _basepath + foldername + "/" + filename + "." + fileextension;
            DirectoryInfo directoryInfo = new DirectoryInfo(_basepath + foldername);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            using (Stream stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            StaticFileInfo entity = new StaticFileInfo
            {
                Extension = fileextension,
                Name = filename,
                ParentEntityId = entityid
            };
            var existing = await _staticFileInfo.GetAllByParentId(entityid, cancellationToken);
            {
                if (existing.Count() > 0)
                {
                    await DeleteAllImagesByParentId(userId, entityid, cancellationToken);
                }
            }

            await _staticFileInfo.Create(userId, entity, cancellationToken);
        }

        public async Task DeleteAllImagesByParentId(Guid userId, Guid id, CancellationToken cancellationToken = default)
        {
            var infos = await _staticFileInfo.GetAllByParentId(id, cancellationToken);
            var basefilepath = _basepath + id.ToString() + "/";

            foreach (var info in infos)
            {
                var filepath = basefilepath + info.Name + "." + info.Extension;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
            await _staticFileInfo.HardDeleteMany(cancellationToken, infos.ToArray());
        }
    }
}