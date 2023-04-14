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
        protected IUserData _userData;
        public BaseRepository(ApplicationDbContext context,IUserData userData)
        {
            _context = context;
            _userData = userData;
        }
        
        public virtual async Task<TEntity?> GetById(Guid Id)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public  IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            return _dbset.Where(e => ids.Contains(e.Id));
        }

        public async Task<PageModelDTO<TEntity>> GetPage(PaginationDTO pagination,IQueryable<TEntity> queryable = null)
        {
           if(queryable == null)
           {
              queryable = _dbset.AsNoTracking();
              queryable = queryable.Where(e => e.IsActive || _userData.Role == Enums.Role.Admin);
           }
            
            return  await queryable.ToPageModelAsync(pagination);
        }

        public virtual async Task<Guid> Create(TEntity entity)
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatedBy = _userData.Id;
            entity.IsActive = true;
            entity.Id = Guid.NewGuid();
            entity.DeleteDateTime = null;
            entity.DeletedBy = null;
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task CreateMany(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateDateTime = DateTime.UtcNow;
                entity.IsActive = true;
                entity.CreatedBy = _userData.Id;
                 entity.DeleteDateTime = null;
            entity.DeletedBy = null;
                await _context.Set<TEntity>().AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatedBy = _userData.Id;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMany(params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.UpdatedBy = _userData.Id;
                _context.Set<TEntity>().Update(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            entity.DeletedBy = _userData.Id;
            entity.DeleteDateTime = DateTime.UtcNow;
     
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMany(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletedBy = _userData.Id;
                entity.DeleteDateTime = DateTime.UtcNow;
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