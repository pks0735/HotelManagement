using AuthService.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthService.Data
{
    public class AuthDbContext:DbContext

    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
