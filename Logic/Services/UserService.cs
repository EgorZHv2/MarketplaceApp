using AutoMapper;
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

        public UserService(
            IUserRepository repository,
            IMapper mapper,
            IUserRepository userRepository,
            IImageService imageService,
            IHashService hashService
        ) : base(repository, mapper)
        {
            _imageService = imageService;
            _hashService = hashService;
        }

        public override async Task<UpdateUserDTO> Update(
            Guid userId,
            UpdateUserDTO model
            
        )
        {
            var user = await _repository.GetById(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (model.Photo != null)
            {
                await _imageService.CreateImage(userId, model.Photo, user.Id);
            }
            var result = await base.Update(userId, model);
            return result;
        }

        public async Task<PageModelDTO<UserDTO>> GetPage(
            Guid userId,
            PaginationDTO pagingModel
            
        )
        {
            var result = new PageModelDTO<UserDTO>();
            var user = await _repository.GetById(userId);
            var qeryable = _repository.GetFiltered(e => (e.IsActive || user.Role == Data.Enums.Role.Admin));
            var pages = await _repository.GetPage(qeryable,pagingModel);
          
                result = _mapper.Map<PageModelDTO<UserDTO>>(pages);
           
          
            return result;
        }

        public async Task<Guid> CreateAdmin(
            Guid userId,
            CreateAdminDTO model
            
        )
        {
            var existing = await _repository.GetUserByEmail(model.Email);
            if (existing != null)
            {
                throw new AlreadyExistsException(
                    "Пользователь с данной почтой уже существует",
                    "This user already exists"
                );
            }
            var user = new UserEntity();

            user = _mapper.Map<UserEntity>(model);

            user.IsEmailConfirmed = true;
            user.Role = Data.Enums.Role.Admin;
            user.Password = _hashService.HashPassword(model.Password);
            await _repository.Create(userId, user);
            return user.Id;
        }
    }
}
