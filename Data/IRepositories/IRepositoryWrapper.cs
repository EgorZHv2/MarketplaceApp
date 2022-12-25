using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IRepositoryWrapper
    {
         IUserRepository User { get; }
        IShopRepository Shop { get; }
        IReviewRepository Review { get; }
        void Save();
    }
}
