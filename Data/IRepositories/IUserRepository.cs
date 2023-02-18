using Data.Entities;

namespace Data.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User?> GetUserByEmail(string email);
    }
}