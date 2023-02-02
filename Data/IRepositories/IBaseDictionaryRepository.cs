using Data.Models;

namespace Data.IRepositories
{
    public interface IBaseDictionaryRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetById(Guid Id, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> GetManyByIds(params Guid[] ids);

        Task<PageModel<TEntity>> GetPage(IQueryable<TEntity> queryable, int pagenumber, int pagesize);

        Task<Guid> Create(Guid userId, TEntity entity, CancellationToken cancellationToken = default);

        Task CreateMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities);

        Task Update(Guid userId, TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities);

        Task Delete(Guid userId, TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities);
    }
}