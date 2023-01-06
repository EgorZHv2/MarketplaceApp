using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
       IEnumerable<TEntity> GetAll();
       TEntity GetById(Guid Id);
       IEnumerable<TEntity> GetManyByIds(params Guid[] ids);
      IEnumerable<TEntity> GetPage(IQueryable queryable,int pagenumber,int pagesize);
        void Create(TEntity entity);
        void CreateMany(params TEntity[] entities);
        void Update(TEntity entity);
        void UpdateMany(params TEntity[] entities);
        void Delete(Guid Id);
        void DeleteMany(params Guid[] ids);
    }
}
