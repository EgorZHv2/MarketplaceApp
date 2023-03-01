using Data.DTO;
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
        public async Task<PageModelDTO<UserEntity>> GetPage (UserEntity user,PaginationDTO paginationDTO)
        {
            var qeryable = _dbset.Where(e => e.IsActive || user.Role == Enums.Role.Admin);
            return await GetPage(paginationDTO, qeryable);
        }
    }
}