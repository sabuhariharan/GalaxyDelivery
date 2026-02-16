using GalaxyDelivery.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.TestHelpers;

public static class DbContextHelper
{
    public static GalaxyDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<GalaxyDbContext>()
            .UseInMemoryDatabase(dbName)
            .EnableSensitiveDataLogging()
            .Options;

        return new GalaxyDbContext(options);
    }
}
