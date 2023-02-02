namespace Logic.Interfaces
{
    public interface IHashService
    {
        public string HashPassword(string str);

        public bool ComparePasswordWithHash(string password, string hash);
    }
}