using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Security
{
    public class JWTGenerator
    {
        public string GenerateToken(string userId)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bubirjwttoken123456bubirjwttoken123456"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("userId", userId)
            };
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "http://localhost:5243",
                audience: "http://localhost:5243",
				claims: claims,
				notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new();
            return handler.WriteToken(token);
		}

    }
}
