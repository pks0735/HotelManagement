using AuthService.Data;
using AuthService.Dto;
using AuthService.Interface;
using AuthService.Model;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository
{
    public class AuthRepository : IAuthService
    {
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(Login login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
            return user;
        }

        public async Task<User> Register(Register register)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == register.Email);
            if (userExists)
            {
                return null;
            }

            var user = new User
            {
                Email = register.Email,
                Role = register.Role,
                Password = register.Password
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
