using Microsoft.EntityFrameworkCore;
using ProductAPI.Core.Models;

namespace ProductAPI.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}