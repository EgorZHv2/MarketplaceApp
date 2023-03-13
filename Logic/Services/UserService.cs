using AutoMapper;
using Data;
using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class UserService
        : BaseService<UserEntity, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository>,
            IUserService
    {
        private IImageService _imageService;
        private IHashService _hashService;
        private IUserData _userData;

        public UserService(
            IUserRepository repository,
            IMapper mapper,
            IImageService imageService,
            IHashService hashService,
            IUserData userData
        ) : base(repository, mapper)
        {
            _imageService = imageService;
            _hashService = hashService;
            _userData = userData;
        }

        public override async Task Update(UpdateUserDTO model)
        {
            var user = await _repository.GetById(_userData.Id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (model.Photo != null)
            {
                await _imageService.CreateImage( model.Photo, user.Id);
            }
            await base.Update(model);
            
        }

        public async Task<PageModelDTO<UserDTO>> GetPage(PaginationDTO pagingModel)
        {
           
            var pages = await _repository.GetPage(pagingModel);
            var result = _mapper.Map<PageModelDTO<UserDTO>>(pages);

            return result;
        }

        public async Task<Guid> CreateAdmin(CreateAdminDTO model)
        {
            var existing = await _repository.GetUserByEmail(model.Email);
            if (existing != null)
            {
                throw new AlreadyExistsException(
                    "Пользователь с данной почтой уже существует",
                    "This user already exists"
                );
            }

            var user = _mapper.Map<UserEntity>(model);
            user.IsEmailConfirmed = true;
            user.Role = Data.Enums.Role.Admin;
            user.Password = _hashService.HashPassword(model.Password);
            await _repository.Create(user);
            return user.Id;
        }
    }
}
