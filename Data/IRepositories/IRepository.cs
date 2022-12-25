using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid Id);
        
    }
}
