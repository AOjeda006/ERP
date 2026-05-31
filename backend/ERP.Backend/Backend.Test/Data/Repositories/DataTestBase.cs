using Data.DataSources;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.Data
{
    public abstract class DataTestBase
    {
        protected ApplicationDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}