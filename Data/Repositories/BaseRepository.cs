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

        protected readonly DbSet<TEntity> _dbset;
        public BaseRepository(DbSet<TEntity> dbset)
        {
            _dbset = dbset;
        }

        public IEnumerable<TEntity> GetAll()
        {
           
            return  _dbset;
        }

        public async Task<TEntity> GetById(Guid Id)
        {
            
            return await _dbset.FindAsync(Id);
        }

        public IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            
            return _dbset.Where(e => ids.Contains(e.Id));;
        }

        public PageModel<TEntity> GetPage(IQueryable<TEntity> queryable,int pagenumber,int pagesize)
        {
            PageModel<TEntity> pageModel = new PageModel<TEntity>
            {
                Values = queryable.Skip(pagenumber - 1).Take(pagesize),
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
            await _dbset.AddAsync(entity);
            
            
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
                await _dbset.FindAsync(entity);
            }
            
        }

        public void Update(TEntity entity,Guid userid)
        {
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userid;
            _dbset.Update(entity);
            
        }

        public void UpdateMany(Guid userid,params TEntity[] entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdateDateTime = DateTime.UtcNow;
                entity.UpdatorId = userid;
                _dbset.Update(entity);
            }
            
        }

        public void Delete(Guid Id,Guid userid)
        {
            var data = _dbset.Find(Id);
            data.DeletorId = userid;
            data.DeleteDateTime = DateTime.UtcNow;
            data.IsDeleted = true;
            _dbset.Update(data);
        }

        public void DeleteMany(Guid userid,params Guid[] ids)
        {
            var data =  _dbset.Where(e => ids.Contains(e.Id));
            foreach (var entity in data)
            {
                entity.DeletorId = userid;
                entity.DeleteDateTime = DateTime.UtcNow;
                entity.IsDeleted = true;
            }
           _dbset.UpdateRange(data);
            
        }

      
    }
}
