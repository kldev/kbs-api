namespace KBS.Web.Services
{
    public class PasswordService : IPasswordService
    {
        public PasswordService()
        {
            
        }

        public string Encrypt(string input)
        {
            return  BCrypt.Net.BCrypt.HashPassword(input);
        }

        public bool Verity(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}