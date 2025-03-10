using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService
{
    public class JwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService(IConfiguration config)
        {
            _key = config["Jwt:Key"];
            _issuer = config["Jwt:Issuer"];
            _audience = config["Jwt:Audience"];
            _expireMinutes = int.Parse(config["Jwt:ExpireMinutes"]);
        }

        public string GenerateToken(string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expireMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
