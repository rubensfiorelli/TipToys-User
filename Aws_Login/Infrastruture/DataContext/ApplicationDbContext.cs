using Aws_Login.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aws_Login.Infrastruture.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
}
