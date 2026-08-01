using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public class AuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher<User>();
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
    }
}
