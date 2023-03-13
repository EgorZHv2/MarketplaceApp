using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, IUserData userData) : base(context, userData)
        {
        }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<PageModelDTO<UserEntity>> GetPage (PaginationDTO paginationDTO)
        {
            var qeryable = _dbset.Where(e=> _userData.Role == Enums.Role.Admin);
            return await GetPage(paginationDTO, qeryable);
        }
    }
}