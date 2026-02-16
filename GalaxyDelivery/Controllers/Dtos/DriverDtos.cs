namespace GalaxyDelivery.Controllers.Dtos;

public sealed class CreateDriverRequestDto
{
    public CreateDriverRequestDto(string driverName)
    {
        DriverName = driverName;
    }

    public string DriverName { get; set; }
}

public sealed class UpdateDriverRequestDto
{
    public UpdateDriverRequestDto(string driverName)
    {
        DriverName = driverName;
    }

    public string DriverName { get; set; }
}
