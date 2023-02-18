using Data.Entities;

namespace Data.IRepositories
{
    public interface IStaticFileInfoRepository : IBaseRepository<StaticFileInfo>
    {
        Task<StaticFileInfo?> GetByParentId(Guid Id);

        Task<IEnumerable<StaticFileInfo>> GetAllByParentId(Guid Id);
    }
}