using Data.DTO;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(Guid Id);

        IEnumerable<TEntity> GetManyByIds(params Guid[] ids);

        Task<PageModelDTO<TEntity>> GetPage(Expression<Func<TEntity, bool>> predicate, PaginationDTO pagination);

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