using GalaxyDelivery.Entities;
using FluentValidation;

namespace GalaxyDelivery.Validators
{
    public class DriverValidator : AbstractValidator<Driver>
    {
        public DriverValidator()
        {
            RuleFor(a => a.DriverName).MaximumLength(100).NotEmpty();
        }
    }
}