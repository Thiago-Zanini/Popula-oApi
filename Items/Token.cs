﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Populacao.Items
{
    public class Token
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _aud;

        public Token(IConfiguration configuration)
        {
            _key = configuration["Jwt:key"];
            _issuer = configuration["Jwt:issuer"];
            _aud = configuration["Jwt:aud"];
        }

        public string GenerateToken(string Nome)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _aud,
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: Claims,
                signingCredentials: credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
