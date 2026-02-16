using GalaxyDelivery.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace GalaxyDelivery.Validators
{
    public class DeliveryEventValidator : AbstractValidator<DeliveryEvent>
    {
        public DeliveryEventValidator(GalaxyDbContext db)
        {
            RuleFor(e => e.DeliveryEventDesc).NotEmpty().MaximumLength(250);
            RuleFor(e => e.Timestamp).NotEmpty();
            RuleFor(e => e.DeliveryId).GreaterThan(0);
            RuleFor(e => e.StatusId).GreaterThan(0);
            RuleFor(e => e.CheckpointId).GreaterThan(0).When(e => e.CheckpointId.HasValue);

            RuleFor(e => e.DeliveryId)
                .MustAsync(async (deliveryId, ct) => await db.Delivery.AsNoTracking().AnyAsync(d => d.DeliveryId == deliveryId, ct))
                .WithMessage("Delivery does not exist.")
                .When(e => e.DeliveryId > 0);

            RuleFor(e => e.StatusId)
                .MustAsync(async (statusId, ct) => await db.DeliveryStatus.AsNoTracking().AnyAsync(s => s.DeliveryStatusId == statusId, ct))
                .WithMessage("Status does not exist.")
                .When(e => e.StatusId > 0);

            RuleFor(e => e.CheckpointId)
                .MustAsync(async (checkpointId, ct) => await db.Checkpoint.AsNoTracking().AnyAsync(c => c.CheckpointId == checkpointId, ct))
                .WithMessage("Checkpoint does not exist.")
                .When(e => e.CheckpointId.HasValue && e.CheckpointId.Value > 0);
        }
    }
}
