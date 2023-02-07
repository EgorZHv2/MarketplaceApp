namespace Data.IRepositories
{
    public interface IBaseDictionaryRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
    }
}