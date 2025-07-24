using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Enums;

namespace MinimalApi.Infra.Db;

public class DbContexto : DbContext
{
    private readonly IConfiguration _configuration;

    public DbContexto(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Cheese> Cheeses { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "admin", Email = "admin@example.com", Password = "123", Profile = ProfileType.ADMIN }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("sqlserver");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'sqlserver' is not configured.");
        }
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                connectionString
            );
        }
    }

}


