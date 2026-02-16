using GalaxyDelivery.Entities;
using FluentValidation;

namespace GalaxyDelivery.Validators
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(v => v.VehicleMake).NotEmpty().MaximumLength(50);
        }
    }
}
