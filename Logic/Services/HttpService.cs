using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class HttpService:IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> HttpGet<T>(string uri) where T : class
        {
            var result = await _httpClient.GetAsync($"{uri}");

            if(!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await FromHttpResponse<T>(result);
        }

        public async Task<T> HttpDelete<T>(string uri,Guid id)where T : class
        {
           var result = await _httpClient.DeleteAsync($"{uri}/{id}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await FromHttpResponse<T>(result);
        }

         public async Task<T> HttpPost<T>(string uri, object dataToSend)
            where T : class
        {
            var content = ToJson(dataToSend);

            var result = await _httpClient.PostAsync($"{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await FromHttpResponse<T>(result);
        }

        public async Task<T> HttpPut<T>(string uri, object dataToSend)
            where T : class
        {
            var content = ToJson(dataToSend);

            var result = await _httpClient.PutAsync($"{uri}", content);
            if (!result.IsSuccessStatusCode)
            {
              return null;
            }

            return await FromHttpResponse<T>(result);
        }


        public StringContent ToJson(object obj) 
        {
            return new StringContent(JsonSerializer.Serialize(obj),Encoding.UTF8,"application/json");
        }
        public async Task<T> FromHttpResponse<T>(HttpResponseMessage result)
        {
            return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}
