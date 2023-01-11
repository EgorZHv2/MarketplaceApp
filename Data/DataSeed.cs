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
            SeedAdmin(repositoryWrapper);
            
        }
        private static void SeedAdmin(IRepositoryWrapper repositoryWrapper)
        {
            var user = repositoryWrapper.Users.GetAll().FirstOrDefault(e => e.Email == AdminMail);
            if (user == null)
            {
                repositoryWrapper.Users.Create(new Entities.User
                {
                    FirstName = "Админ",
                    Email = AdminMail,
                    Password = BCrypt.Net.BCrypt.HashPassword(AdminPassword),
                    Role = Enums.Role.Admin,
                    Id = Guid.NewGuid()
                });
                repositoryWrapper.Save();
            }
        }

    }
}
