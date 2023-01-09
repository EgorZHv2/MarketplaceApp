using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Interfaces
{
    public interface IHttpService
    {
        Task<T> HttpGet<T>(string uri) where T : class;
        Task<T> HttpDelete<T>(string uri, Guid id) where T : class;
        Task<T> HttpPost<T>(string uri, object dataToSend) where T : class;
        Task<T> HttpPut<T>(string uri, object dataToSend) where T : class;
        StringContent ToJson(object obj);
        Task<T> FromHttpResponse<T>(HttpResponseMessage result);
    }
}
