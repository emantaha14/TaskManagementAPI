using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace TaskManagementApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            var exists = await _dbContext.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists) return false;
            var User = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };

            User.PasswordHash = _passwordHasher.HashPassword(User, dto.Password);
            _dbContext.Users.Add(User);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string?> Login(LoginDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync( u => u.Email == dto.Email);
            if (user == null) return null;
            var result = _passwordHasher.VerifyHashedPassword(
                  user,
                  user.PasswordHash,
                  dto.Password
                );

            if(result == PasswordVerificationResult.Failed) return null;
            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
         };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
