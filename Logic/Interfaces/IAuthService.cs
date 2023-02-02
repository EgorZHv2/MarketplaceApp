using Data.DTO.Auth;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        public Task VerifyEmail(string email, string code, CancellationToken cancellationToken = default);

        public Task<string> Login(LoginDTO model, CancellationToken cancellationToken = default);

        public Task Register(RegistrationDTO model, CancellationToken cancellationToken = default);

        public Task ChangePassword(Guid userId, ChangePasswordDTO model, CancellationToken cancellationToken = default);
    }
}