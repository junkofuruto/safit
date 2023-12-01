using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Safit.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Safit.Core.DataAccess;

public class DatabaseContext : DbContext
{
    private IConfiguration configuration;

    public DatabaseContext(IConfiguration configuration) 
    {
        this.configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration["Safit:Database:ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>( entity => { entity.HasIndex(e => e.Username).IsUnique(); });
    }

    public DbSet<User> Users { get; set; }
}