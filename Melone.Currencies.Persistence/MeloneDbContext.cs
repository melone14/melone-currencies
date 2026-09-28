using Melone.Currencies.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Melone.Currencies.Persistence;

public class MeloneDbContext : DbContext
{
    public MeloneDbContext(DbContextOptions<MeloneDbContext> options) : base(options)
    {  
    }

    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeloneDbContext).Assembly);
    }
}
