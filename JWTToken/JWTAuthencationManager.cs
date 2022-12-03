using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTToken
{
    public class JWTAuthencationManager : IJWTAuthencationManager
    {
        private readonly string _key;
        public JWTAuthencationManager(string key)
        {
            _key = key;
        }


        private readonly IDictionary<string, string> user = new Dictionary<string, string>()
        {
            {"user1","pwd1"},
            {"user2","pwd2" }
        };
        public string Authenticate(string username, string password)
        {
            if (!user.Any(x => x.Key == username && x.Value == password))
            {
                return null;
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var TokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(TokenDescription);
            return tokenhandler.WriteToken(token);
        }
    }
}
