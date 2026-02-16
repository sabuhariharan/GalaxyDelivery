using GalaxyDelivery.Entities;
using FluentValidation;

namespace GalaxyDelivery.Validators
{
    public class CheckpointValidator : AbstractValidator<Checkpoint>
    {
        public CheckpointValidator()
        {
            RuleFor(c => c.CheckpointName).NotEmpty().MaximumLength(50);
            RuleFor(c => c.RouteId).GreaterThan(0);
        }
    }
}
