using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; // Claim için gerekli!
using System.Text;
using TodoApp.Application.Abstractions.Token;
using TodoApp.Domain.Entities.Identity; // AppUser için (kendi yoluna göre düzeltirsin)

namespace TodoApp.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // DİKKAT: Buraya AppUser parametresi ekledik!
        public Application.DTOs.Token CreateAccessToken(int minute, AppUser user)
        {
            Application.DTOs.Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(minute);

            // 1. TOKEN'IN İÇİNE GİZLEYECEĞİMİZ KİMLİK BİLGİLERİ (CLAIMS)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ID'yi basıyoruz
                new Claim(ClaimTypes.Name, user.UserName) // İstersen adını da basabilirsin
            };

            JwtSecurityToken securityToken = new JwtSecurityToken(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                claims: claims, // 2. OLUŞTURDUĞUMUZ KİMLİĞİ TOKEN'A VERİYORUZ
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            return token;
        }

        // Kullanmadığın hatalı interface metodunu şimdilik sildim, kafayı karıştırmasın.
    }
}