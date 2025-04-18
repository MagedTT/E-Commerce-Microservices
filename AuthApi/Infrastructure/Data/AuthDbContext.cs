using AuthAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
}