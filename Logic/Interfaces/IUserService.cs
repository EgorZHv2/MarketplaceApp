using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IUserService : IBaseService<UserEntity, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository>
    {
        public Task<UpdateUserDTO> Update(Guid userId, UpdateUserDTO DTO);

        public Task<Guid> CreateAdmin(Guid userId, CreateAdminDTO model);
    }
}