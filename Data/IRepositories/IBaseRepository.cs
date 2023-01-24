using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
       IEnumerable<TEntity> GetAll();
       Task<TEntity> GetById(Guid Id,CancellationToken cancellationToken = default);
       IEnumerable<TEntity> GetManyByIds(params Guid[] ids);
        Task<PageModel<TEntity>> GetPage(IQueryable<TEntity> queryable, int pagenumber, int pagesize);
        Task<Guid> Create(TEntity entity, CancellationToken cancellationToken = default);
        Task CreateMany(Guid userid,params TEntity[] entities);
        void Update(TEntity entity);
        void UpdateMany(Guid userid,params TEntity[] entities);
        void Delete(Guid Id,Guid userid);
        void DeleteMany(Guid userid,params Guid[] ids);
    }
}
