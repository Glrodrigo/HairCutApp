using HairCut.Generals;
using HairCut.Tools.Domain;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HairCut.Tools.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private IConfiguration _configuration;

        public AuthenticateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserBase> AuthenticateAsync(string email, string password, List<UserBase> user)
        {
            try
            {
                if (user[0].Active == false || user[0].ExclusionDate != null)
                    return null;

                using var hmac = new HMACSHA512(user[0].PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user[0].PasswordHash[i])
                    {
                        user[0].PasswordAttemptCount = user[0].PasswordAttemptCount + 1;
                        return null;
                    }
                }

                return user.FirstOrDefault();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public string GenerateToken(int id, string email)
        {
            var newId = Guid.NewGuid();

            try
            {
                var claims = new[]
                {
                    new Claim("id", id.ToString()),
                    new Claim("email", email),
                    new Claim(JwtRegisteredClaimNames.Jti, newId.ToString())
                };

                var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.UtcNow.AddHours(8);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                var hashToken = new JwtSecurityTokenHandler().WriteToken(token);

                return hashToken;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public PasswordByte PasswordByteAsync(UserBase user)
        {
            try
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                byte[] PasswordSalt = hmac.Key;

                PasswordByte passwordByte = new PasswordByte() { PasswordHash = passwordHash, PasswordSalt = PasswordSalt };

                return passwordByte;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
