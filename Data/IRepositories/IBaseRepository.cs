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
        Task<Guid> Create(Guid userId,TEntity entity, CancellationToken cancellationToken = default);
        Task CreateMany(Guid userid,CancellationToken cancellationToken = default,params TEntity[] entities);
        Task Update(Guid userId, TEntity entity,CancellationToken cancellationToken = default);
        Task UpdateMany(Guid userid,CancellationToken cancellationToken = default,params TEntity[] entities);
        Task Delete(Guid userId,Guid entityId, CancellationToken cancellationToken = default);
        Task DeleteMany(Guid userid,CancellationToken cancellationToken = default,params Guid[] ids);
    }
}
