using System;
using System.Collections.Generic;

namespace GalaxyDelivery.Controllers.Dtos;

public sealed class DeliveryEventItemDto
{
    public DeliveryEventItemDto(int deliveryEventId, string deliveryEventDesc, DateTime timestamp, int deliveryId, string checkpointName, string statusName)
    {
        DeliveryEventId = deliveryEventId;
        DeliveryEventDesc = deliveryEventDesc;
        Timestamp = timestamp;
        DeliveryId = deliveryId;
        CheckpointName = checkpointName;
        StatusName = statusName;
    }

    public int DeliveryEventId { get; set; }

    public string DeliveryEventDesc { get; set; }

    public DateTime Timestamp { get; set; }

    public int DeliveryId { get; set; }

    public string CheckpointName { get; set; }

    public string StatusName { get; set; }
}

public sealed class DeliveryListItemDto
{
    public DeliveryListItemDto(int deliveryId, string origin, string destination, string driverName, string vehicleMake, string routeName, IEnumerable<DeliveryEventItemDto> deliveryEvents)
    {
        DeliveryId = deliveryId;
        Origin = origin;
        Destination = destination;
        DriverName = driverName;
        VehicleMake = vehicleMake;
        RouteName = routeName;
        DeliveryEvents = deliveryEvents;
    }

    public int DeliveryId { get; set; }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public string DriverName { get; set; }

    public string VehicleMake { get; set; }

    public string RouteName { get; set; }

    public IEnumerable<DeliveryEventItemDto> DeliveryEvents { get; set; }
}

public sealed class DeliveryDetailDto
{
    public DeliveryDetailDto(int deliveryId, string origin, string destination, string driverName, string vehicleMake, string routeName, IEnumerable<DeliveryEventItemDto> deliveryEvents)
    {
        DeliveryId = deliveryId;
        Origin = origin;
        Destination = destination;
        DriverName = driverName;
        VehicleMake = vehicleMake;
        RouteName = routeName;
        DeliveryEvents = deliveryEvents;
    }

    public int DeliveryId { get; set; }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public string DriverName { get; set; }

    public string VehicleMake { get; set; }

    public string RouteName { get; set; }

    public IEnumerable<DeliveryEventItemDto> DeliveryEvents { get; set; }
}

public sealed class DeliverySummaryDto
{
    public DeliverySummaryDto(int deliveryId, string origin, string destination, string routeName, string driverName, string vehicleMake, TimeSpan timeTaken, IEnumerable<DeliverySummaryEventItemDto> events)
    {
        DeliveryId = deliveryId;
        Origin = origin;
        Destination = destination;
        RouteName = routeName;
        DriverName = driverName;
        VehicleMake = vehicleMake;
        TimeTaken = timeTaken;
        Events = events;
    }

    public int DeliveryId { get; set; }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public string RouteName { get; set; }

    public string DriverName { get; set; }

    public string VehicleMake { get; set; }

    public TimeSpan TimeTaken { get; set; }

    public IEnumerable<DeliverySummaryEventItemDto> Events { get; set; }
}

public sealed class DeliverySummaryEventItemDto
{
    public DeliverySummaryEventItemDto(string deliveryEventDesc, DateTime timestamp, string checkpointName, string statusName)
    {
        DeliveryEventDesc = deliveryEventDesc;
        Timestamp = timestamp;
        CheckpointName = checkpointName;
        StatusName = statusName;
    }

    public string DeliveryEventDesc { get; set; }

    public DateTime Timestamp { get; set; }

    public string CheckpointName { get; set; }

    public string StatusName { get; set; }
}

public sealed class CreateDeliveryRequestDto
{
    public CreateDeliveryRequestDto(string origin, string destination, int driverId, int vehicleId, int routeId)
    {
        Origin = origin;
        Destination = destination;
        DriverId = driverId;
        VehicleId = vehicleId;
        RouteId = routeId;
    }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public int RouteId { get; set; }
}

public sealed class UpdateDeliveryRequestDto
{
    public UpdateDeliveryRequestDto(string origin, string destination, int driverId, int vehicleId, int routeId)
    {
        Origin = origin;
        Destination = destination;
        DriverId = driverId;
        VehicleId = vehicleId;
        RouteId = routeId;
    }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public int DriverId { get; set; }

    public int VehicleId { get; set; }

    public int RouteId { get; set; }
}
