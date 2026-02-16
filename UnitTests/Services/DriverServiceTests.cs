using GalaxyDelivery.Entities;
using GalaxyDelivery.Services;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestHelpers;

namespace UnitTests.Services;

[TestFixture]
public class DriverServiceTests
{
    [Test]
    public async Task CreateDriverAsync_WhenValid_AddsDriver()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(CreateDriverAsync_WhenValid_AddsDriver));

        var validator = new Mock<IValidator<Driver>>();
        validator.Setup(v => v.Validate(It.IsAny<Driver>())).Returns(new ValidationResult());

        var sut = new DriverService(db, validator.Object);

        var created = await sut.CreateDriverAsync(new Driver { DriverName = "Test" });

        Assert.That(created.DriverId, Is.GreaterThanOrEqualTo(0));
        Assert.That(await db.Driver.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetDriverAsync_WhenMissing_ReturnsNull()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(GetDriverAsync_WhenMissing_ReturnsNull));

        var validator = new Mock<IValidator<Driver>>();
        validator.Setup(v => v.Validate(It.IsAny<Driver>())).Returns(new ValidationResult());

        var sut = new DriverService(db, validator.Object);

        var driver = await sut.GetDriverAsync(123);

        Assert.That(driver, Is.Null);
    }
}
