using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IHttpService
    {
        Task<T> PostAsync<T>(string uri, object content) where T : class;
        Task<T> GetAsync<T>(string uri) where T : class;
        Task<T> PutAsync<T>(string uri, object content) where T : class;
        Task<T> DeleteAsync<T>(string uri, Guid id) where T : class;
    }
}
