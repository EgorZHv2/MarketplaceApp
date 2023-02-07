using AutoMapper;
using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class UserService : BaseService<User, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository>, IUserService
    {
        private IImageService _imageService;
        private IHashService _hashService;

        public UserService(IUserRepository repository, IMapper mapper, IUserRepository userRepository, IImageService imageService, IHashService hashService) : base(repository, mapper)
        {
            _imageService = imageService;
            _hashService = hashService;
        }

        public override async Task<UpdateUserDTO> Update(Guid userId, UpdateUserDTO model, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetById(userId);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            if (model.Photo != null)
            {
                await _imageService.CreateImage(userId, model.Photo, user.Id);
            }
            var result = await base.Update(userId, model, cancellationToken);
            return result;
        }

        public async Task<PageModelDTO<UserDTO>> GetPage(Guid userId, FilterPagingDTO pagingModel, CancellationToken cancellationToken = default)
        {
            var result = new PageModelDTO<UserDTO>();
            var user = await _repository.GetById(userId);
            var pages = await _repository.GetPage(e => (e.IsActive || user.Role == Data.Enums.Role.Admin), pagingModel.PageNumber, pagingModel.PageSize, cancellationToken);
            try
            {
                result = _mapper.Map<PageModelDTO<UserDTO>>(pages);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }

        public async Task<Guid> CreateAdmin(Guid userId, CreateAdminDTO model, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetUserByEmail(model.Email, cancellationToken);
            if (existing != null)
            {
                throw new AlreadyExistsException("Пользователь с данной почтой уже существует", "This user already exists");
            }
            User user = new User();

            try
            {
                user = _mapper.Map<User>(model);
            }
            catch
            {
                throw new MappingException(this);
            }
            user.IsEmailConfirmed = true;
            user.Role = Data.Enums.Role.Admin;
            user.Password = _hashService.HashPassword(model.Password);
            await _repository.Create(userId, user, cancellationToken);
            return user.Id;
        }
    }
}