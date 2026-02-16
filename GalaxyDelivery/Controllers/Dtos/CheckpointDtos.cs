namespace GalaxyDelivery.Controllers.Dtos;

public sealed class CreateCheckpointRequestDto
{
    public CreateCheckpointRequestDto(string checkpointName, int routeId)
    {
        CheckpointName = checkpointName;
        RouteId = routeId;
    }

    public string CheckpointName { get; set; }

    public int RouteId { get; set; }
}

public sealed class UpdateCheckpointRequestDto
{
    public UpdateCheckpointRequestDto(string checkpointName, int routeId)
    {
        CheckpointName = checkpointName;
        RouteId = routeId;
    }

    public string CheckpointName { get; set; }

    public int RouteId { get; set; }
}
