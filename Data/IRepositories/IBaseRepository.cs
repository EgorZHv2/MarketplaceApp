using Data.Models;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetById(Guid Id, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> GetManyByIds(params Guid[] ids);

        Task<PageModel<TEntity>> GetPage(Expression<Func<TEntity, bool>> predicate, int pagenumber, int pagesize, CancellationToken cancellationToken = default);

        Task<Guid> Create(Guid userId, TEntity entity, CancellationToken cancellationToken = default);

        Task CreateMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities);

        Task Update(Guid userId, TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities);

        Task Delete(Guid userId, TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities);
    }
}