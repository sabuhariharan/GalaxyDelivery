using GalaxyDelivery.Entities;
using FluentValidation;

namespace GalaxyDelivery.Validators
{
    public class DeliveryStatusValidator : AbstractValidator<DeliveryStatus>
    {
        public DeliveryStatusValidator()
        {
            RuleFor(s => s.StatusName).NotEmpty().MaximumLength(50);
        }
    }
}
