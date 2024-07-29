using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class TokenHelper
    {
        private static readonly string Key = "UiL2gcwicdbhoTEFakwbOiGjRaPAofaSQcI7NZq6q0H8anzXOZbYhlGRzLHpBdNI";
        private static readonly string Issue = "PE4OgEyhqXtep4RJWlPxZ7O4ytJG7Htl";
        private static readonly string Audience = "QaAAxChpaR2rtLO9O8BiXnweqFHDwuRj";
        public static string GenerateToken(string account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issue,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static bool ValidationToken(string token)
        {
            if (token == null) return false;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issue,
                ValidAudience = Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
