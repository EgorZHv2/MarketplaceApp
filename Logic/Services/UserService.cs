using AutoMapper;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class UserService : BaseService<User, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository>, IUserService
    {
        private IUserRepository _userRepository;
        private IImageService _imageService;

        public UserService(IUserRepository repository, IMapper mapper, IUserRepository userRepository, IImageService imageService) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _imageService = imageService;
        }

        public override async Task<UpdateUserDTO> Update(Guid userId, UpdateUserDTO model, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            if (model.Photo != null)
            {
                await _imageService.CreateImage(model.Photo, user.Id);
            }
            var result = await base.Update(userId, model, cancellationToken);
            return result;
        }
    }
}