namespace GalaxyDelivery.Controllers.Dtos;

public sealed class CreateRouteRequestDto
{
    public CreateRouteRequestDto(string routeName)
    {
        RouteName = routeName;
    }

    public string RouteName { get; set; }
}

public sealed class UpdateRouteRequestDto
{
    public UpdateRouteRequestDto(string routeName)
    {
        RouteName = routeName;
    }

    public string RouteName { get; set; }
}
