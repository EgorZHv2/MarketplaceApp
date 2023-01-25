using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class DataSeed
    {
        private const string AdminMail = "admin@mail.ru";
        private const string AdminPassword = "12345678";
        public static void SeedData(IRepositoryWrapper repositoryWrapper)
        {
            SeedAdmin(repositoryWrapper).Wait();
            
        }
        public  static async Task SeedAdmin(IRepositoryWrapper repositoryWrapper)
        {
            var user = await repositoryWrapper.Users.GetUserByEmail(AdminMail);
            if (user == null)
            {
              await repositoryWrapper.Users.Create(new Entities.User
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
                repositoryWrapper.Save();
            }
        }

    }
}
