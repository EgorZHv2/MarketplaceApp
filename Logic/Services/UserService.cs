﻿using AutoMapper;
using Data.DTO;
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
        private IHashService _hashService;

        public UserService(IUserRepository repository, IMapper mapper, IUserRepository userRepository, IImageService imageService, IHashService hashService) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _hashService = hashService;
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
                await _imageService.CreateImage(userId,model.Photo, user.Id);
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
        public async Task<Guid> CreateAdmin(Guid userId, CreateAdminDTO model, CancellationToken cancellationToken = default)
        {
            User user = new User();
            try
            {
                user = _mapper.Map<User>(model);
            }
            catch
            {
                throw new MappingException(this);
            }
            user.Role = Data.Enums.Role.Admin;
            user.Password = _hashService.HashPassword(model.Password);
            await _repository.Create(userId, user,cancellationToken);
            return user.Id;
        }
    }
}