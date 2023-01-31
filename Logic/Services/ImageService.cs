using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ImageService:IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ImageService> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ImageService(IConfiguration configuration,
            ILogger<ImageService> logger,
            IRepositoryWrapper repositoryWrapper) 
        {
            _configuration = configuration;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task CreateImage(IFormFile file, Guid entityid,CancellationToken cancellationToken = default)
        {
            string fileextension = file.FileName.Split(".").Last();
            if (!(fileextension == "png" || fileextension == "jpg" || fileextension == "jpeg"))
            {
                throw new WrongExtensionException("Картинка может быть только а png, jpg или jpeg", "Wrong image extension");
            }
            string filename = Guid.NewGuid().ToString();            
            string foldername = entityid.ToString();
            string filepath = _configuration.GetSection("BaseImagePath").Value + foldername+"/" + filename + "." + fileextension;
            DirectoryInfo directoryInfo = new DirectoryInfo(_configuration.GetSection("BaseImagePath").Value + foldername);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            using(Stream stream = new FileStream(filepath, FileMode.Create))
            {
                _logger.LogError(filepath);
                await file.CopyToAsync(stream);
            }
            StaticFileInfo entity = new StaticFileInfo
            {
                Extension = fileextension,
                Name = filename,
                ParentEntityId = entityid
            };
            _repositoryWrapper.StaticFileInfos.Create(Guid.Empty,entity,cancellationToken);
            _repositoryWrapper.Save();
        }
    }
}
