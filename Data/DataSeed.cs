using Data.IRepositories;

namespace Data
{
    public class DataSeed
    {
        private const string AdminMail = "admin@mail.ru";
        private const string AdminPassword = "12345678";

        public static void SeedData(IUserRepository userRepository)
        {
            SeedAdmin(userRepository).Wait();
        }

        public static async Task SeedAdmin(IUserRepository userRepository)
        {
            var user = await userRepository.GetUserByEmail(AdminMail);
            if (user == null)
            {
                await userRepository.Create(Guid.Empty, new Entities.User
                {
                    FirstName = "Админ",
                    Email = AdminMail,
                    Password = BCrypt.Net.BCrypt.HashPassword(AdminPassword),
                    IsEmailConfirmed = true,
                    Role = Enums.Role.Admin,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    IsDeleted = false
                });
            }
        }
    }
}