namespace KBS.Web.Services
{
    public interface IPasswordService
    {
        string Encrypt(string input);
        bool Verity(string password, string hash);
    }
}