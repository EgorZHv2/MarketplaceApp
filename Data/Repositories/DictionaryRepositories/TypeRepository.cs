using Data.IRepositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class TypeRepository : BaseDictionaryRepository<Data.Entities.Type>, ITypeRepository
    {
        public TypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}