using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}