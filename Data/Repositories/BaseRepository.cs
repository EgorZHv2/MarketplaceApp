using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        protected readonly ApplicationDbContext _context;
        protected DbSet<TEntity> _dbset => _context.Set<TEntity>();
        public BaseRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public IEnumerable<TEntity> GetAll()
        {

            return _dbset;
        }

        public async Task<TEntity> GetById(Guid Id,CancellationToken cancellationToken =default)
        {
            
            return await _dbset.FirstOrDefaultAsync(e=>e.Id == Id,cancellationToken);
        }

        public IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            
            return _context.Set<TEntity>().Where(e => ids.Contains(e.Id));
        }

        public virtual async Task<PageModel<TEntity>> GetPage(IQueryable<TEntity> queryable,int pagenumber,int pagesize)
        {
            PageModel<TEntity> pageModel = new PageModel<TEntity>
            {
                Values = await queryable.Skip(pagenumber - 1).Take(pagesize).ToListAsync(),
                ItemsOnPage = pagesize,
                CurrentPage = pagenumber,
                TotalItems = queryable.Count(),
                TotalPages = (int)Math.Ceiling(queryable.Count() /(double)pagesize)
            };
            return pageModel;
        }

        public async Task<Guid> Create(Guid userId,TEntity entity,CancellationToken cancellationToken = default)
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatorId = userId;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.Id = Guid.NewGuid();
            await _context.Set<TEntity>().AddAsync(entity,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task CreateMany(Guid userid,CancellationToken cancellationToken = default,params TEntity[] entities)
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
                await _context.Set<TEntity>().AddAsync(entity,cancellationToken);
                
            }
            await _context.SaveChangesAsync(cancellationToken);         
        }

        public async Task Update(Guid userId, TEntity entity,CancellationToken cancellationToken = default)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
        }

        public async Task UpdateMany(Guid userid,CancellationToken cancellationToken = default,params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.UpdatorId = userid;
                _context.Set<TEntity>().Update(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
            
        }

        public async Task Delete(Guid userid,Guid entityid,CancellationToken cancellationToken = default)
        {
            var data = _context.Set<TEntity>().Find(entityid);
            data.DeletorId = userid;
            data.DeleteDateTime = DateTime.UtcNow;
            data.IsDeleted = true;
            _context.Set<TEntity>().Update(data);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMany(Guid userid,CancellationToken cancellationToken = default,params Guid[] ids)
        {
            var data =  _context.Set<TEntity>().Where(e => ids.Contains(e.Id));
            foreach (var entity in data)
            {
                entity.DeletorId = userid;
                entity.DeleteDateTime = DateTime.UtcNow;
                entity.IsDeleted = true;
            }
            _context.Set<TEntity>().UpdateRange(data);
            await  _context.SaveChangesAsync(cancellationToken);
        }

      
    }
}
