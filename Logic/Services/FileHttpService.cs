using Data;
using Data.DTO;
using Data.Options;
using Logic.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class FileHttpService:BaseHttpService,IFileHttpService
    {
        private readonly ApplicationOptions _options;
        private readonly IUserData _userData;
        public FileHttpService(HttpClient client, IOptions<ApplicationOptions> options,
            IUserData userData) : base(client) 
        {
            _options = options.Value;
            _userData= userData;
        }

        public async Task<FileInfoDTO> PostAsync(CreateFileDTO dto)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.EntityId.ToString()),"EntityId");
            form.Add(new StreamContent(dto.File.OpenReadStream()), "File", dto.File.FileName);
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(_options.FileControllerPath,UriKind.RelativeOrAbsolute),
                Method = HttpMethod.Post,
                Content = form,
                
            };
            httpRequest.Headers.Add("Authorization", "Bearer " + _userData.JWToken);
            var response = await _httpClient.SendAsync(httpRequest);
            var result = await FromHttpResponseMessage<FileInfoDTO>(response);
            return result;
        }
        public async Task<FileInfoDTO> PostFileFromLinkAsync(CreateFileFromLinkDTO dto)
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.EntityId.ToString()),"EntityId");
            form.Add(new StringContent(dto.FileLink), "FileLink");
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(_options.FileControllerPath +"/from-link",UriKind.RelativeOrAbsolute),
                Method = HttpMethod.Post,
                Content = form
            };
            httpRequest.Headers.Add("Authorization", "Bearer " + _userData.JWToken);
            var response = await _httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode + await response.Content.ReadAsStringAsync());
            }
            var result = await FromHttpResponseMessage<FileInfoDTO>(response);
            return result;
        }
        public async Task DeleteFilesByParentId(Guid parentId)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(_options.FileControllerPath + "/by-parent-id/" + parentId.ToString(), UriKind.RelativeOrAbsolute),
                Method = HttpMethod.Delete
            };
           httpRequest.Headers.Add("Authorization", "Bearer " + _userData.JWToken);
            var response = await _httpClient.SendAsync(httpRequest);
            
        }

    }
}
