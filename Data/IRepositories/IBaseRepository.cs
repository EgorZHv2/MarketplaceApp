using Data.DTO;
using Data.Entities;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(Guid Id);

        IEnumerable<TEntity> GetManyByIds(params Guid[] ids);

        Task<PageModelDTO<TEntity>> GetPage(PaginationDTO pagination, IQueryable<TEntity> queryable = null);

        Task<Guid> Create(TEntity entity);

        Task CreateMany(params TEntity[] entities);

        Task Update(TEntity entity);

        Task UpdateMany(params TEntity[] entities);

        Task Delete(TEntity entity );

        Task DeleteMany(params TEntity[] entities);

        Task HardDelete(TEntity entity );

        Task HardDeleteMany(params TEntity[] entities);
    }
}