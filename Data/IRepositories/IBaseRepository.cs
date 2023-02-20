using Data.DTO;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(Guid Id);

        IEnumerable<TEntity> GetManyByIds(params Guid[] ids);
        IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);

        Task<PageModelDTO<TEntity>> GetPage(IQueryable<TEntity> queryable, PaginationDTO pagination);

        Task<Guid> Create(Guid userId, TEntity entity);

        Task CreateMany(Guid userId,  params TEntity[] entities);

        Task Update(Guid userId, TEntity entity);

        Task UpdateMany(Guid userId, params TEntity[] entities);

        Task Delete(Guid userId, TEntity entity );

        Task DeleteMany(Guid userId,params TEntity[] entities);

        Task HardDelete(TEntity entity );

        Task HardDeleteMany(params TEntity[] entities);
    }
}