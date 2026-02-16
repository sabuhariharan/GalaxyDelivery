using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Services.Interfaces;

public interface ICheckpointService
{
    Task<IEnumerable<Checkpoint>> GetCheckpointsAsync();

    Task<Checkpoint> GetCheckpointAsync(int checkpointId);

    Task<Checkpoint> CreateCheckpointAsync(Checkpoint checkpoint);

    Task<Checkpoint> UpdateCheckpointAsync(Checkpoint checkpoint);

    Task DeleteCheckpointAsync(int checkpointId);
}
