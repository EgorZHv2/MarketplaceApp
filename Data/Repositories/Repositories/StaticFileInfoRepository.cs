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
    public class StaticFileInfoRepository:BaseRepository<StaticFileInfo>, IStaticFileInfoRepository
    {
        public StaticFileInfoRepository(ApplicationDbContext context):base(context) { }
        public async Task<StaticFileInfo> GetByParentId(Guid Id)
        {
            var file = await _context.Set<StaticFileInfo>().FirstOrDefaultAsync(e => e.ParentEntityId == Id);
            return file;
        }
    }
}
