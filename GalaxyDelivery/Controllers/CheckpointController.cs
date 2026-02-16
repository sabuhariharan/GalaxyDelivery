using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GalaxyDelivery.Controllers;

[ApiController]
[Route("v1/api/Checkpoints")]
[Produces("application/json")]
public class CheckpointController(ICheckpointService checkpointService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CheckpointDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CheckpointDto>>> GetCheckpoints()
    {
        var checkpoints = await checkpointService.GetCheckpointsAsync();
        var vms = checkpoints.Select(c => new CheckpointDto(c.CheckpointId, c.CheckpointName, c.Route?.RouteName));

        return Ok(vms);
    }

    [HttpGet]
    [Route("{checkpointId:int}")]
    [ProducesResponseType(typeof(CheckpointDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckpointDto>> GetCheckpoint([FromRoute] int checkpointId)
    {
        var checkpoint = await checkpointService.GetCheckpointAsync(checkpointId);
        if (checkpoint is null)
        {
            return NotFound();
        }

        return Ok(new CheckpointDto(checkpoint.CheckpointId, checkpoint.CheckpointName, checkpoint.Route?.RouteName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CheckpointDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CheckpointDto>> PostCheckpoint([FromBody] CreateCheckpointRequestDto request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var checkpoint = new Checkpoint
        {
            CheckpointName = request.CheckpointName,
            RouteId = request.RouteId
        };

        var created = await checkpointService.CreateCheckpointAsync(checkpoint);
        var full = await checkpointService.GetCheckpointAsync(created.CheckpointId);
        var createdVm = new CheckpointDto(full.CheckpointId, full.CheckpointName, full.Route?.RouteName);

        return CreatedAtAction(nameof(GetCheckpoint), new { checkpointId = createdVm.CheckpointId }, createdVm);
    }

    [HttpPut]
    [Route("{checkpointId:int}")]
    [ProducesResponseType(typeof(CheckpointDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckpointDto>> PutCheckpoint([FromRoute] int checkpointId, [FromBody] UpdateCheckpointRequestDto request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var existing = await checkpointService.GetCheckpointAsync(checkpointId);
        if (existing is null)
        {
            return NotFound();
        }

        existing.CheckpointName = request.CheckpointName;
        existing.RouteId = request.RouteId;

        var updated = await checkpointService.UpdateCheckpointAsync(existing);
        var full = await checkpointService.GetCheckpointAsync(updated.CheckpointId);
        return Ok(new CheckpointDto(full.CheckpointId, full.CheckpointName, full.Route?.RouteName));
    }

    [HttpDelete]
    [Route("{checkpointId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCheckpoint([FromRoute] int checkpointId)
    {
        await checkpointService.DeleteCheckpointAsync(checkpointId);
        return NoContent();
    }
}
