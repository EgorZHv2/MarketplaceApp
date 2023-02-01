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
    public class PostgreShopRepository : BaseRepository<Shop>,IShopRepository
    {       
       public  PostgreShopRepository(ApplicationDbContext context):base(context) { }
        public override async Task<Guid> Create(Guid userId,Shop entity,CancellationToken cancellationToken = default)
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatorId = userId;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.Id = Guid.NewGuid();
            entity.SellerId = userId;
            await _dbset.AddAsync(entity,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
       
    }
}
