using GalaxyDelivery.Entities;
using GalaxyDelivery.Services;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestHelpers;

namespace UnitTests.Services;

[TestFixture]
public class CheckpointServiceTests
{
    [Test]
    public async Task DeleteCheckpointAsync_WhenExists_Removes()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(DeleteCheckpointAsync_WhenExists_Removes));
        db.Checkpoint.Add(new Checkpoint { CheckpointId = 1, CheckpointName = "A", RouteId = 1 });
        await db.SaveChangesAsync();

        var sut = new CheckpointService(db);

        await sut.DeleteCheckpointAsync(1);

        Assert.That(await db.Checkpoint.CountAsync(), Is.EqualTo(0));
    }
}
