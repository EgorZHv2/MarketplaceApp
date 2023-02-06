using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.DictionaryRepositories
{
    public class BaseDictionaryRepository<TEntity> : BaseRepository<TEntity>,  IBaseDictionaryRepository<TEntity> where TEntity : BaseDictionaryEntity
    {
        public BaseDictionaryRepository(ApplicationDbContext context):base(context) { }
    }
}