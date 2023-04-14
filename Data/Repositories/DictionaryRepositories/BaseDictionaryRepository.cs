using Data.Entities;
using Data.IRepositories;
using Data.Repositories.Repositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class BaseDictionaryRepository<TEntity> : BaseRepository<TEntity>, IBaseDictionaryRepository<TEntity> where TEntity : BaseDictionaryEntity
    {
        public BaseDictionaryRepository(ApplicationDbContext context, IUserData userData) : base(context, userData)
        {
        }
    }
}