using AutoMapper;
using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class UserService:BaseService<User,UserDTO,UserDTO,BaseUpdateDTO,IUserRepository>
    {

        public UserService(IUserRepository repository,IMapper mapper):base(repository,mapper)
        {

        }
    }
}
