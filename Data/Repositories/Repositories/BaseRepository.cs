using Data.DTO;
using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<TEntity> _dbset => _context.Set<TEntity>();

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }
        public async Task<TEntity?> GetById(Guid Id)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public  IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            return _dbset.Where(e => ids.Contains(e.Id));
        }

        public async Task<PageModelDTO<TEntity>> GetPage(IQueryable<TEntity> queryable, PaginationDTO pagination)
        {
           
            
            return  await queryable.ToPageModelAsync(pagination.PageNumber, pagination.PageSize);
        }

        public virtual async Task<Guid> Create(Guid userId, TEntity entity)
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatorId = userId;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.Id = Guid.NewGuid();
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task CreateMany(Guid userId,params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateDateTime = DateTime.UtcNow;
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsDeleted = false;
                entity.CreatorId = userId;
                entity.UpdatorId = userId;
                await _context.Set<TEntity>().AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid userId, TEntity entity)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMany(Guid userId,params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.UpdatorId = userId;
                _context.Set<TEntity>().Update(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid userId, TEntity entity)
        {
            entity.DeletorId = userId;
            entity.DeleteDateTime = DateTime.UtcNow;
            entity.IsDeleted = true;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMany(Guid userId, params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletorId = userId;
                entity.DeleteDateTime = DateTime.UtcNow;
                entity.IsDeleted = true;
            }
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task HardDelete(TEntity entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task HardDeleteMany(params TEntity[] entities)
        {
            _dbset.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}