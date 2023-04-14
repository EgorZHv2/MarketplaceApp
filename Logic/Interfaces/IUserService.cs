using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IUserService : IBaseService<UserEntity, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository>
    {
        public Task Update(UpdateUserDTO DTO);

        public Task<Guid> CreateAdmin(CreateAdminDTO model);
    }
}