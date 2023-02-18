using Data.DTO.Auth;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        public Task VerifyEmail(string email, string code);

        public Task<string> Login(LoginDTO model);

        public Task Register(RegistrationDTO model);

        public Task ChangePassword(Guid userId, ChangePasswordDTO model);
    }
}