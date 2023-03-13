using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.DictionaryRepositories
{
    public class TypeRepository : BaseDictionaryRepository<TypeEntity>, ITypeRepository
    {
        public TypeRepository(ApplicationDbContext context, IUserData userData) : base(context, userData)
        {
        }
    }
}