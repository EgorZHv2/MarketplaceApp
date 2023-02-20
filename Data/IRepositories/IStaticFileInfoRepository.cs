using Data.Entities;

namespace Data.IRepositories
{
    public interface IStaticFileInfoRepository : IBaseRepository<StaticFileInfoEntity>
    {
        Task<StaticFileInfoEntity?> GetByParentId(Guid Id);

        Task<IEnumerable<StaticFileInfoEntity>> GetAllByParentId(Guid Id);
    }
}