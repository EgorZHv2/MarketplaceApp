using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IUserService:IBaseService<User,UserDTO,CreateUserDTO,UpdateUserDTO,IUserRepository>
    {
        public Task<UpdateUserDTO> Update(Guid userId, UpdateUserDTO DTO, CancellationToken cancellationToken = default);
    }
}
