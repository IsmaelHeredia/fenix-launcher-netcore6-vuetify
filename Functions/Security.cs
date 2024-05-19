using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FenixLauncher.Functions
{
    public class Security
    {
        string jwt_key = new Configuration().jwt_key;
        string jwt_issuer = new Configuration().jwt_issuer;
        string jwt_audience = new Configuration().jwt_audience;

        public bool ValidateTokenLogin(string headers)
        {
            bool response = false;
            string[] split_auth = headers.Split("Bearer ");
            if (split_auth.Length > 1)
            {
                string jwt_token = split_auth[1];
                if (ValidateSessionJWT(jwt_token))
                {
                    response = true;
                }
            }
            return response;
        }

        public string GenerateTokenJWT(int id, string usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(id)),
                new Claim(ClaimTypes.Name, usuario)
            };

            var token = new JwtSecurityToken(
                jwt_issuer,
                jwt_audience,
                claims,
                expires: DateTime.Now.AddMinutes(3600),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int LoadTokenId(string token)
        {
            var tokenS = new JwtSecurityToken(token);
            var id = tokenS.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Convert.ToInt32(id);
        }

        public string loadTokenName(string token)
        {
            var tokenS = new JwtSecurityToken(token);
            var name = tokenS.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            return name;
        }

        public bool ValidateSessionJWT(string token)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt_key));

            TokenValidationParameters options = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = jwt_issuer,
                ValidAudience = jwt_audience
            };

            if (ValidateToken(token, options))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateToken(string token, TokenValidationParameters options)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                ClaimsPrincipal principal = handler.ValidateToken(token, options, out securityToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
