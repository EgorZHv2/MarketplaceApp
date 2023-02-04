using AutoMapper;
using Data.DTO.Shop;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
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

        public  async Task<PageModel<UserDTO>> GetPage(Guid userId, FilterPagingModel pagingModel, CancellationToken cancellationToken = default)
        {
            var result = new PageModel<UserDTO>();
            var user = await _userRepository.GetById(userId);
            var pages = await _repository.GetPage(e => (e.IsActive || user.Role == Data.Enums.Role.Admin), pagingModel.PageNumber, pagingModel.PageSize, cancellationToken);
            try
            {
                result = _mapper.Map<PageModel<UserDTO>>(pages);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }
    }
}