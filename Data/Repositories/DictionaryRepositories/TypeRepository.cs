using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.DictionaryRepositories
{
    public class TypeRepository:BaseDictionaryRepository<Data.Entities.Type>,ITypeRepository
    {
        public TypeRepository(ApplicationDbContext context): base(context) { }
    }
}
