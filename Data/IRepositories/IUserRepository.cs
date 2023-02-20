using Data.Entities;

namespace Data.IRepositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        public Task<UserEntity?> GetUserByEmail(string email);
    }
}