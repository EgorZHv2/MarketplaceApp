using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Data.Models;
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

       
        public async Task<TEntity> GetById(Guid Id, CancellationToken cancellationToken = default)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
        }

        public IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            return _context.Set<TEntity>().Where(e => ids.Contains(e.Id));
        }

      

        public async Task<PageModel<TEntity>> GetPage(Expression<Func<TEntity, bool>> predicate, int pagenumber, int pagesize,CancellationToken cancellationToken = default)
        {
            var queryable = _dbset.Where(predicate);
            var result = await queryable.ToPageModelAsync(pagenumber,pagesize, cancellationToken);
            return result;
        }

        public virtual async Task<Guid> Create(Guid userId, TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatorId = userId;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.Id = Guid.NewGuid();
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task CreateMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateDateTime = DateTime.UtcNow;
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsDeleted = false;
                entity.CreatorId = userid;
                entity.UpdatorId = userid;
                await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Guid userId, TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.UpdatorId = userid;
                _context.Set<TEntity>().Update(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid userid, TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.DeletorId = userid;
            entity.DeleteDateTime = DateTime.UtcNow;
            entity.IsDeleted = true;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMany(Guid userid, CancellationToken cancellationToken = default, params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletorId = userid;
                entity.DeleteDateTime = DateTime.UtcNow;
                entity.IsDeleted = true;
            }
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task HardDelete(TEntity entity,CancellationToken cancellationToken = default)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
         public async Task HardDeleteMany(CancellationToken cancellationToken = default, params TEntity[] entities)
        {
            _dbset.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}