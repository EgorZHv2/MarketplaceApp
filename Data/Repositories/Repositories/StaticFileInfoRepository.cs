using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Repositories
{
    public class StaticFileInfoRepository : BaseRepository<StaticFileInfo>, IStaticFileInfoRepository
    {
        public StaticFileInfoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<StaticFileInfo?> GetByParentId(Guid Id)
        {
            
            return await _dbset.FirstOrDefaultAsync(e => e.ParentEntityId == Id);
        }

        public async Task<IEnumerable<StaticFileInfo>> GetAllByParentId(Guid Id)
        {
           
            return await _dbset.Where(e => e.ParentEntityId == Id).ToListAsync();
        }
    }
}