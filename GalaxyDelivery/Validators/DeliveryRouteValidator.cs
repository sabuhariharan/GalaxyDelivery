using GalaxyDelivery.Entities;
using FluentValidation;

namespace GalaxyDelivery.Validators
{
    public class DeliveryRouteValidator : AbstractValidator<DeliveryRoute>
    {
        public DeliveryRouteValidator()
        {
            RuleFor(r => r.RouteId).GreaterThan(0);
            RuleFor(r => r.RouteName).NotEmpty().MaximumLength(100);
        }
    }
}
