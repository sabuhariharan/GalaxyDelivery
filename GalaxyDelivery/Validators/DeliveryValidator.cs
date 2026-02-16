using GalaxyDelivery.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace GalaxyDelivery.Validators
{
    public class DeliveryValidator : AbstractValidator<Delivery>
    {
        public DeliveryValidator(GalaxyDbContext db)
        {
            RuleFor(d => d.Origin).NotEmpty().MaximumLength(100);
            RuleFor(d => d.Destination).NotEmpty().MaximumLength(100);
            RuleFor(d => d.DriverId).GreaterThan(0);
            RuleFor(d => d.VehicleId).GreaterThan(0);
            RuleFor(d => d.RouteId).GreaterThan(0);

            RuleFor(d => d.DriverId)
                .MustAsync(async (driverId, ct) => await db.Driver.AsNoTracking().AnyAsync(x => x.DriverId == driverId, ct))
                .WithMessage("Driver does not exist.")
                .When(d => d.DriverId > 0);

            RuleFor(d => d.VehicleId)
                .MustAsync(async (vehicleId, ct) => await db.Vehicle.AsNoTracking().AnyAsync(x => x.VehicleId == vehicleId, ct))
                .WithMessage("Vehicle does not exist.")
                .When(d => d.VehicleId > 0);

            RuleFor(d => d.RouteId)
                .MustAsync(async (routeId, ct) => await db.Route.AsNoTracking().AnyAsync(x => x.RouteId == routeId, ct))
                .WithMessage("Route does not exist.")
                .When(d => d.RouteId > 0);
        }
    }
}
