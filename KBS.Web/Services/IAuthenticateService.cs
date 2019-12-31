using KBS.Web.Model;

namespace KBS.Web.Services {
    public interface IAuthenticateService {
        bool IsAuthenticated(AuthRequest auth, out AuthResponse response);
    }
}
