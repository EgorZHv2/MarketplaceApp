using Logic.Interfaces;

namespace Logic.Services
{
    public class HashService : IHashService
    {
        public string HashPassword(string str)
        {
            return BCrypt.Net.BCrypt.HashPassword(str); ;
        }

        public bool ComparePasswordWithHash(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}