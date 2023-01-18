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
    public class PostgreStaticFileInfoRepository:BaseRepository<StaticFileInfo>, IStaticFileInfoRepository
    {
        public PostgreStaticFileInfoRepository(ApplicationDbContext context):base(context.StaticFileInfos) { }
        public async Task<StaticFileInfo> GetByParentId(Guid Id)
        {
            var file = await _dbset.FirstOrDefaultAsync(e => e.ParentEntityId == Id);
            return file;
        }
    }
}
