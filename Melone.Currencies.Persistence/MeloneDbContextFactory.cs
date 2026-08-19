using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Melone.Currencies.Persistence;

public class MeloneDbContextFactory : IDesignTimeDbContextFactory<MeloneDbContext>
{
    public MeloneDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MeloneDbContext>();

        optionsBuilder.UseSqlite("Data Source=MeloneCurrencies.db");

        return new MeloneDbContext(optionsBuilder.Options);
    }
}
