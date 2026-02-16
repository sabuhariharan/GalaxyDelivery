using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GalaxyDelivery.Services;

public class CheckpointService(GalaxyDbContext db) : ICheckpointService
{
    public async Task<IEnumerable<Checkpoint>> GetCheckpointsAsync()
    {
        return await db.Checkpoint.AsNoTracking()
            .Include(c => c.Route)
            .ToListAsync();
    }

    public async Task<Checkpoint> GetCheckpointAsync(int checkpointId)
    {
        return await db.Checkpoint.AsNoTracking()
            .Include(c => c.Route)
            .SingleOrDefaultAsync(c => c.CheckpointId == checkpointId);
    }

    public async Task<Checkpoint> CreateCheckpointAsync(Checkpoint checkpoint)
    {
        db.Checkpoint.Add(checkpoint);
        await db.SaveChangesAsync();
        return checkpoint;
    }

    public async Task<Checkpoint> UpdateCheckpointAsync(Checkpoint checkpoint)
    {
        db.Checkpoint.Update(checkpoint);
        await db.SaveChangesAsync();
        return checkpoint;
    }

    public async Task DeleteCheckpointAsync(int checkpointId)
    {
        var existing = await db.Checkpoint.SingleOrDefaultAsync(c => c.CheckpointId == checkpointId);
        if (existing is null)
        {
            return;
        }

        db.Checkpoint.Remove(existing);
        await db.SaveChangesAsync();
    }
}
