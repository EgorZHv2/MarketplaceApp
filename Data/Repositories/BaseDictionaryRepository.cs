using Data.IRepositories;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories
{
    public class BaseDictionaryRepository<TEntity>:IBaseDictionaryRepository<TEntity> where TEntity : BaseDictionaryEntity
    {
        protected readonly ApplicationDbContext _context;
        public BaseDictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
           
            return  _context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(Guid Id)
        {
            
            return await _context.Set<TEntity>().FindAsync(Id);
        }

        public IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            
            return _context.Set<TEntity>().Where(e => ids.Contains(e.Id));;
        }

        public PageModel<TEntity> GetPage(IQueryable<TEntity> queryable,int pagenumber,int pagesize)
        {
            PageModel<TEntity> pageModel = new PageModel<TEntity>
            {
                Values =  queryable.Skip(pagesize*(pagenumber-1)).Take(pagesize),
                ItemsOnPage = pagesize,
                CurrentPage = pagenumber,
                TotalItems = queryable.Count(),
                TotalPages = (int)Math.Ceiling(queryable.Count() /(double)pagesize)
            };
            return pageModel;
        }

        public async Task Create(TEntity entity,Guid userid)
        {
            entity.Id = Guid.NewGuid();
            entity.CreateDateTime = DateTime.UtcNow;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.CreatorId = userid;
            entity.UpdatorId = userid;
             await _context.Set<TEntity>().AddAsync(entity);
           
            
            
        }

        public async Task CreateMany(Guid userid,params TEntity[] entities)
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
                await _context.Set<TEntity>().FindAsync(entity);
            }
            
        }

        public void Update(TEntity entity,Guid userid)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userid;
            _context.Set<TEntity>().Update(entity);
            
        }

        public void UpdateMany(Guid userid,params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.UpdatorId = userid;
                _context.Set<TEntity>().Update(entity);
            }
            
        }

        public void Delete(Guid Id,Guid userid)
        {
            var data = _context.Set<TEntity>().Find(Id);
            data.DeletorId = userid;
            data.DeleteDateTime = DateTime.UtcNow;
            data.IsDeleted = true;
            _context.Set<TEntity>().Update(data);
        }

        public void DeleteMany(Guid userid,params Guid[] ids)
        {
            var data =  _context.Set<TEntity>().Where(e => ids.Contains(e.Id));
            foreach (var entity in data)
            {
                entity.DeletorId = userid;
                entity.DeleteDateTime = DateTime.UtcNow;
                entity.IsDeleted = true;
            }
           _context.Set<TEntity>().UpdateRange(data);
            
        }
    }
}
