using Azure;
using System;
using System.Collections.Generic;

namespace GalaxyDelivery.Controllers.Dtos;

//CommonDtos.cs contains resource/response DTOs (what the API returns), e.g.DriverDto, VehicleDto, CheckpointDto, etc.

public sealed class DriverDto
{
    public DriverDto(int driverId, string driverName)
    {
        DriverId = driverId;
        DriverName = driverName;
    }

    public int DriverId { get; set; }

    public string DriverName { get; set; }
}

public sealed class VehicleDto
{
    public VehicleDto(int vehicleId, string vehicleMake)
    {
        VehicleId = vehicleId;
        VehicleMake = vehicleMake;
    }

    public int VehicleId { get; set; }

    public string VehicleMake { get; set; }
}

public sealed class CheckpointDto
{
    public CheckpointDto(int checkpointId, string checkpointName, string routeName)
    {
        CheckpointId = checkpointId;
        CheckpointName = checkpointName;
        RouteName = routeName;
    }

    public int CheckpointId { get; set; }

    public string CheckpointName { get; set; }

    public string RouteName { get; set; }
}

public sealed class DeliveryRouteDto
{
    public DeliveryRouteDto(int routeId, string routeName, IEnumerable<CheckpointDto> checkpoints)
    {
        RouteId = routeId;
        RouteName = routeName;
        Checkpoints = checkpoints;
    }

    public int RouteId { get; set; }

    public string RouteName { get; set; }

    public IEnumerable<CheckpointDto> Checkpoints { get; set; }
}

public sealed class DeliveryEventDto
{
    public DeliveryEventDto(int deliveryEventId, string deliveryEventDesc, DateTime timestamp, int deliveryId, int? checkpointId, string checkpointName, int statusId, string statusName)
    {
        DeliveryEventId = deliveryEventId;
        DeliveryEventDesc = deliveryEventDesc;
        Timestamp = timestamp;
        DeliveryId = deliveryId;
        CheckpointId = checkpointId;
        CheckpointName = checkpointName;
        StatusId = statusId;
        StatusName = statusName;
    }

    public int DeliveryEventId { get; set; }

    public string DeliveryEventDesc { get; set; }

    public DateTime Timestamp { get; set; }

    public int DeliveryId { get; set; }

    public int? CheckpointId { get; set; }

    public string CheckpointName { get; set; }

    public int StatusId { get; set; }

    public string StatusName { get; set; }
}
