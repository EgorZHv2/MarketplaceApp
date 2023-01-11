using Data.Entities;
using Data.IRepositories;
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

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var data =  _context.Set<TEntity>();
            return data;
        }

        public TEntity GetById(Guid Id)
        {
            var data =  _context.Set<TEntity>().Find(Id);
            return data;
        }

        public IEnumerable<TEntity> GetManyByIds(params Guid[] ids)
        {
            var data = _context.Set<TEntity>().Where(e => ids.Contains(e.Id));
            return data;
        }

        public IEnumerable<TEntity> GetPage(
            IQueryable queryable,
            int pagenumber,
            int pagesize
        )
        {
            var data =  _context.Set<TEntity>();
            return data;
        }

        public void Create(TEntity entity,Guid userid)
        {
            entity.Id = Guid.NewGuid();
            entity.CreateDateTime = DateTime.UtcNow;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.CreatorId = userid;
            entity.UpdatorId = userid;
            _context.Set<TEntity>().Add(entity);
            
            
        }

        public void CreateMany(Guid userid,params TEntity[] entities)
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
                _context.Set<TEntity>().Add(entity);
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
            _context.Set<TEntity>().Remove(data);
        }

        public void DeleteMany(Guid userid,params Guid[] ids)
        {
            var data =  _context.Set<TEntity>().Where(e => ids.Contains(e.Id));
            foreach (var entity in data)
            {
                entity.DeletorId = userid;
                entity.DeleteDateTime = DateTime.UtcNow;
            }
            _context.Set<TEntity>().RemoveRange(data);
            
        }
    }
}
