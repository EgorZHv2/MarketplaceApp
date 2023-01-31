using Data.IRepositories;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Exceptions;
using Data.Entities;
using Data.DTO.Auth;
using WebAPi.Interfaces;
using AutoMapper;
using System.Net;

namespace Logic.Services
{
    public class AuthService : IAuthService
    {
        private IHashService _hashService;
        private IUserRepository _userRepository;
        private ITokenService _tokenService;
        private IMapper _mapper;
        private IRandomStringGeneratorService _stringGeneratorService;
        private IEmailService _emailService;

        public AuthService(
            IUserRepository userRepository,
            IHashService hashService,
            ITokenService tokenService,
            IMapper mapper,
            IRandomStringGeneratorService stringGeneratorService,
            IEmailService emailService
        )
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _mapper = mapper;
            _stringGeneratorService = stringGeneratorService;
            _emailService = emailService;
        }

        public async Task VerifyEmail(
            string email,
            string code,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _userRepository.GetUserByEmail(email,cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Email не найден", "User email not found");
            }
            if (!_hashService.ComparePasswordWithHash(code, user.EmailConfirmationCode))
            {
                throw new AuthException(
                    "Неправильный код",
                    "Wrong email verification code",
                    System.Net.HttpStatusCode.Unauthorized,
                    user.Email
                );
            }
            user.IsEmailConfirmed = true;
            await _userRepository.Update(user, cancellationToken);
        }

        public async Task<string> Login(
            LoginDTO model,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _userRepository.GetUserByEmail(model.Email, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Email не найден", "User email not found");
            }
            if (!user.IsEmailConfirmed)
            {
                throw new AuthException(
                    "Почта не подтверждёна",
                    "Email not verified",
                    System.Net.HttpStatusCode.Forbidden,
                    model.Email
                );
            }
            if (!_hashService.ComparePasswordWithHash(model.Password, user.Password))
            {
                throw new AuthException(
                    "Неверный пароль",
                    "Wrong password",
                    System.Net.HttpStatusCode.Unauthorized,
                    model.Email
                );
            }
            var result = _tokenService.GetToken(user);
            return result;
        }

        public async Task Register(
            RegistrationDTO model,
            CancellationToken cancellationToken = default
        )
        {
            var dbuser = await _userRepository.GetUserByEmail(model.Email, cancellationToken);
            if (dbuser != null)
            {
                throw new AuthException(
                    "Email занят",
                    "Email already in use",
                    System.Net.HttpStatusCode.Unauthorized,
                    string.Empty
                );
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
            string code = _stringGeneratorService.Generate(6);
            user.Password =  _hashService.HashPassword(model.Password);
            user.EmailConfirmationCode =  _hashService.HashPassword(code);
            _emailService.SendEmail(
                user.Email,
                "MarketPlaceApp",
                $"Your verification code = {code}",
                cancellationToken
            );
            await _userRepository.Create(user, cancellationToken);
        }

         public async Task ChangePassword(Guid userId,ChangePasswordDTO model,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _userRepository.GetById(userId,cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
           
            if (!_hashService.ComparePasswordWithHash(model.OldPassword,user.Password))
            {
                throw new AuthException(
                    "Старый пароль неверный",
                    "Wrong old password",
                    HttpStatusCode.Unauthorized,
                    user.Email
                );
            }
            user.Password = _hashService.HashPassword(model.Password);
            await _userRepository.Update(user,cancellationToken);
        }
    }
}
