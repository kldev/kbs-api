using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KBS.Web.Data;
using KBS.Web.Infrastructure;
using KBS.Web.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KBS.Web.Services {
    public class TokenAuthenticateService : IAuthenticateService {
        private readonly JwtConfig config;
        private readonly UserRepository userRepository;
        private readonly IPasswordService _passwordService;



        public TokenAuthenticateService(IOptions<JwtConfig> options, IConnectionFactory factory, IPasswordService passwordService) {
            config = options.Value;
            userRepository = new UserRepository (factory);
            _passwordService = passwordService;
        }

        public bool IsAuthenticated(AuthRequest auth, out AuthResponse response) {
            response = null;

            if (auth == null
                || string.IsNullOrEmpty (auth.Username)
                || string.IsNullOrEmpty (auth.Password)) {
                return false;
            }

            var user = userRepository.GetByUsernameAsync (auth.Username).Result;

            if (user != null && _passwordService.Verity (auth.Password, user.Password)) {

                var claim = new[]
                {
                    new Claim(ClaimTypes.Name, auth.Username),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Role", user.Role.ToString())
                };

                var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (config.Secret));
                var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken (
                    null,
                    null,
                    claim,
                    expires: DateTime.Now.AddHours (8),
                    signingCredentials: credentials
                );
                var token = new JwtSecurityTokenHandler ( ).WriteToken (jwtToken);
                response = new AuthResponse {
                    Token = token,
                    Role = user.Role.ToString ( )

                };

                return true;
            }

            return false;
        }
    }
}
