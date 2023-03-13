using AutoMapper;
using Data;
using Data.DTO.Auth;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using System.Net;
using System.Text.RegularExpressions;
using WebAPi.Interfaces;

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
        private IUserData _userData;

        public AuthService(
            IUserRepository userRepository,
            IHashService hashService,
            ITokenService tokenService,
            IMapper mapper,
            IRandomStringGeneratorService stringGeneratorService,
            IEmailService emailService,
            IUserData userData
            
        )
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _mapper = mapper;
            _stringGeneratorService = stringGeneratorService;
            _emailService = emailService;
            _userData = userData;
        }

        public async Task VerifyEmail(string email, string code)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new WrongEmailAddressException();
            }
            if (!_hashService.ComparePasswordWithHash(code, user.EmailConfirmationCode))
            {
                throw new WrongVerificationCodeException();
            }
            user.IsEmailConfirmed = true;
            await _userRepository.Update(user);
        }

        public async Task<string> Login(LoginDTO model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                throw new WrongEmailAddressException();
            }
            if (!user.IsEmailConfirmed)
            {
                throw new EmailNotConfirmedException();
            }
            if (!_hashService.ComparePasswordWithHash(model.Password, user.Password))
            {
                throw new WrongPasswordException();
            }
            var result = _tokenService.GetToken(user);
            return result;
        }

        public async Task Register(RegistrationDTO model)
        {
            var dbuser = await _userRepository.GetUserByEmail(model.Email);
            if (dbuser != null)
            {
                throw new EmailInUseException();
            }
            string code = _stringGeneratorService.Generate(6);
            var user = _mapper.Map<UserEntity>(model);
            user.Password = _hashService.HashPassword(model.Password);
            user.EmailConfirmationCode = _hashService.HashPassword(code);
            _emailService.SendEmail(
                user.Email,
                "MarketPlaceApp",
                $"Your verification code = {code}"
            );
            await _userRepository.Create(user);
        }

        public async Task ChangePassword(ChangePasswordDTO model)
        {
            var user = await _userRepository.GetById(_userData.Id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!_hashService.ComparePasswordWithHash(model.OldPassword, user.Password))
            {
                throw new WrongPasswordException();
            }
            user.Password = _hashService.HashPassword(model.Password);
            await _userRepository.Update(user);
        }
    }
}
