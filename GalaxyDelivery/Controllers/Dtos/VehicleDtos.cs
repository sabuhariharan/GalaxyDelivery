namespace GalaxyDelivery.Controllers.Dtos;

public sealed class CreateVehicleRequestDto
{
    public CreateVehicleRequestDto(string vehicleMake)
    {
        VehicleMake = vehicleMake;
    }

    public string VehicleMake { get; set; }
}

public sealed class UpdateVehicleRequestDto
{
    public UpdateVehicleRequestDto(string vehicleMake)
    {
        VehicleMake = vehicleMake;
    }

    public string VehicleMake { get; set; }
}
