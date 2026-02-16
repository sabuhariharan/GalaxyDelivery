using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GalaxyDelivery.Entities
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GalaxyDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<GalaxyDbContext>>()))
            {
                context.DeliveryStatus.AddRange(
                    new DeliveryStatus { DeliveryStatusId = (int)DeliveryStatusCode.Planned, StatusName = DeliveryStatusCodeNames.GetName(DeliveryStatusCode.Planned) },
                    new DeliveryStatus { DeliveryStatusId = (int)DeliveryStatusCode.Started, StatusName = DeliveryStatusCodeNames.GetName(DeliveryStatusCode.Started) },
                    new DeliveryStatus { DeliveryStatusId = (int)DeliveryStatusCode.InProgress, StatusName = DeliveryStatusCodeNames.GetName(DeliveryStatusCode.InProgress) },
                    new DeliveryStatus { DeliveryStatusId = (int)DeliveryStatusCode.Completed, StatusName = DeliveryStatusCodeNames.GetName(DeliveryStatusCode.Completed) },
                    new DeliveryStatus { DeliveryStatusId = (int)DeliveryStatusCode.Failed, StatusName = DeliveryStatusCodeNames.GetName(DeliveryStatusCode.Failed) });

                // Add Drivers
                context.Driver.AddRange(
                    new Driver
                    {
                        DriverId = 1,
                        DriverName = "Captain Nova"
                    },
                    new Driver
                    {
                        DriverId = 2,
                        DriverName = "Commander Stellar"
                    },
                    new Driver
                    {
                        DriverId = 3,
                        DriverName = "Pilot Orion"
                    });

                // Add Vehicles
                context.Vehicle.AddRange(
                    new Vehicle
                    {
                        VehicleId = 1,
                        VehicleMake = "HoverTruck"
                    },
                    new Vehicle
                    {
                        VehicleId = 2,
                        VehicleMake = "RocketVan"
                    },
                    new Vehicle
                    {
                        VehicleId = 3,
                        VehicleMake = "SpaceCycle"
                    });

                // Add Routes
                context.Route.AddRange(
                    new DeliveryRoute
                    {
                        RouteId = 1,
                        RouteName = "Route 1"
                    },
                    new DeliveryRoute
                    {
                        RouteId = 2,
                        RouteName = "Route 2"
                    },
                    new DeliveryRoute
                    {
                        RouteId = 3,
                        RouteName = "Route 3"
                    });

                // Add Checkpoints
                context.Checkpoint.AddRange(
                    new Checkpoint
                    {
                        CheckpointId = 1,
                        CheckpointName = "Earth Central Hub",
                        RouteId = 1
                    },
                    new Checkpoint
                    {
                        CheckpointId = 2,
                        CheckpointName = "Mars Station Alpha",
                        RouteId = 1
                    },

                    new Checkpoint
                    {
                        CheckpointId = 3,
                        CheckpointName = "Jupiter Colony",
                        RouteId = 1
                    },

                    new Checkpoint
                    {
                        CheckpointId = 4,
                        CheckpointName = "Saturn Ring Port",
                        RouteId = 2
                    },
                    new Checkpoint
                    {
                        CheckpointId = 5,
                        CheckpointName = "Venus Orbital",
                        RouteId = 2
                    },
                    new Checkpoint
                    {
                        CheckpointId = 6,
                        CheckpointName = "Neptune Outpost",
                        RouteId = 3
                    },


                    new Checkpoint
                    {
                        CheckpointId = 7,
                        CheckpointName = "Colony A",
                        RouteId = 3
                    },

                    new Checkpoint
                    {
                        CheckpointId = 8,
                        CheckpointName = "Colony B",
                        RouteId = 3
                    },

                    new Checkpoint
                    {
                       CheckpointId = 9,
                       CheckpointName = "Colony C",
                       RouteId = 3
                    }

                    );

                // Add Deliveries
                context.Delivery.AddRange(
                    new Delivery
                    {
                        DeliveryId = 1,
                        Origin = "Earth Central Hub",
                        Destination = "Jupiter Colony",
                        DriverId = 1,
                        VehicleId = 1,
                        RouteId = 1
                    },
                    new Delivery
                    {
                        DeliveryId = 2,
                        Origin = "Moon Base",
                        Destination = "Saturn Mining Station",
                        DriverId = 2,
                        VehicleId = 2,
                        RouteId = 2
                    },
                    new Delivery
                    {
                        DeliveryId = 3,
                        Origin = "Mars Colony",
                        Destination = "Neptune Research Facility",
                        DriverId = 3,
                        VehicleId = 3,
                        RouteId = 3
                    },
                    new Delivery
                    {
                        DeliveryId = 4,
                        Origin = "Pluto Colony",
                        Destination = "Neptune Research Facility",
                        DriverId = 3,
                        VehicleId = 3,
                        RouteId = 3
                    }
                    );

                // Add DeliveryEvents
                context.DeliveryEvent.AddRange(
                    new DeliveryEvent
                    {
                        DeliveryEventId = 1,
                        DeliveryEventDesc = "Package picked up from Earth Central Hub",
                        Timestamp = DateTime.UtcNow.AddMinutes(-90),
                        DeliveryId = 1,
                        CheckpointId = 1,
                        StatusId = (int)DeliveryStatusCode.Started
                    },
                    new DeliveryEvent
                    {
                        DeliveryEventId = 2,
                        DeliveryEventDesc = "Passed through Mars Station Alpha checkpoint",
                        Timestamp = DateTime.UtcNow.AddMinutes(-60),
                        DeliveryId = 1,
                        CheckpointId = 2,
                        StatusId = (int)DeliveryStatusCode.InProgress
                    },
                    new DeliveryEvent
                    {
                        DeliveryEventId = 3,
                        DeliveryEventDesc = "Package delivered to Jupiter Colony",
                        Timestamp = DateTime.UtcNow.AddMinutes(-30),
                        DeliveryId = 1,
                        CheckpointId = 3,
                        StatusId = (int)DeliveryStatusCode.Completed
                    },
                    new DeliveryEvent
                    {
                        DeliveryEventId = 4,
                        DeliveryEventDesc = "Package picked up from Moon Base",
                        Timestamp = DateTime.UtcNow.AddMinutes(-120),
                        DeliveryId = 2,
                        CheckpointId = null,
                        StatusId = (int)DeliveryStatusCode.Started
                    },
                    new DeliveryEvent
                    {
                        DeliveryEventId = 5,
                        DeliveryEventDesc = "Arrived at Venus Orbital checkpoint",
                        Timestamp = DateTime.UtcNow.AddMinutes(-80),
                        DeliveryId = 2,
                        CheckpointId = 4,
                        StatusId = (int)DeliveryStatusCode.InProgress
                    },
                    new DeliveryEvent
                    {
                        DeliveryEventId = 6,
                        DeliveryEventDesc = "Package picked up from Mars Colony",
                        Timestamp = DateTime.UtcNow.AddMinutes(-45),
                        DeliveryId = 3,
                        CheckpointId = 2,
                        StatusId = (int)DeliveryStatusCode.Started
                    },
                    new DeliveryEvent
                    {
                        DeliveryEventId = 7,
                        DeliveryEventDesc = "Deliver planned to Neptune from Pluto",
                        Timestamp = DateTime.UtcNow.AddMinutes(-45),
                        DeliveryId = 4,
                        CheckpointId = null,
                        StatusId = (int)DeliveryStatusCode.Planned
                    }

                    );

                context.SaveChanges();
            }
        }
    }
}