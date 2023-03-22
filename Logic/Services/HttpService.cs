using Data.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Logic.Interfaces;
using System.Net.Http.Json;

namespace Logic.Services
{
    public class HttpService:IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> PostAsync<T>(string uri, object content) where T : class
        {
            var json = ToJson(content);
            var result = await _httpClient.PostAsync(uri, json);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.StatusCode + await result.Content.ReadAsStringAsync());
            }
            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> GetAsync<T>(string uri) where T : class
        {
            var result = await _httpClient.GetAsync($"{uri}");
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Заглушка http ошибки");
            }

            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> PutAsync<T>(string uri,object content) where T:class 
        {
            var json = ToJson(content);
            var result = await _httpClient.PutAsync(uri, json);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Заглушка http ошибки");
            }
            return await FromHttpResponseMessage<T>(result);
        }

        public async Task<T> DeleteAsync<T>(string uri, Guid id)  where T:class 
        {
            var result = await _httpClient.DeleteAsync($"{uri}/{id}");
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Заглушка http ошибки");
            }

            return await FromHttpResponseMessage<T>(result);
        }

        private StringContent ToJson(object obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                "application/json"
            );
        }

        private async Task<T> FromHttpResponseMessage<T>(HttpResponseMessage result)
        {
            return JsonSerializer.Deserialize<T>(
                await result.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true }
            );
        }
    }
}
