using System;
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
[Route("v1/api/DeliveryEvents")]
[Produces("application/json")]
public class DeliveryEventController(IDeliveryEventService deliveryEventService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeliveryEventResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeliveryEventResponseDto>>> GetDeliveryEvents()
    {
        var events = await deliveryEventService.GetDeliveryEventsAsync();
        var vms = events.Select(e => new DeliveryEventResponseDto(
            e.DeliveryEventId,
            e.DeliveryEventDesc,
            e.Timestamp,
            e.DeliveryId,
            e.Checkpoint?.CheckpointName,
            e.Status?.StatusName));

        return Ok(vms);
    }

    [HttpGet]
    [Route("{deliveryEventId:int}")]
    [ProducesResponseType(typeof(DeliveryEventResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryEventResponseDto>> GetDeliveryEvent([FromRoute] int deliveryEventId)
    {
        var ev = await deliveryEventService.GetDeliveryEventAsync(deliveryEventId);
        if (ev is null)
        {
            return NotFound();
        }

        return Ok(new DeliveryEventResponseDto(
            ev.DeliveryEventId,
            ev.DeliveryEventDesc,
            ev.Timestamp,
            ev.DeliveryId,
            ev.Checkpoint?.CheckpointName,
            ev.Status?.StatusName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeliveryEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeliveryEventDto>> PostDeliveryEvent([FromBody] CreateDeliveryEventRequestDto request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var ev = new DeliveryEvent
        {
            DeliveryEventDesc = request.DeliveryEventDesc,
            Timestamp = request.Timestamp ?? DateTime.UtcNow,
            DeliveryId = request.DeliveryId,
            CheckpointId = request.CheckpointId,
            StatusId = request.StatusId
        };

        var created = await deliveryEventService.CreateDeliveryEventAsync(ev);
        var createdVm = new DeliveryEventDto(
            created.DeliveryEventId,
            created.DeliveryEventDesc,
            created.Timestamp,
            created.DeliveryId,
            created.CheckpointId,
            null,
            created.StatusId,
            null);

        return CreatedAtAction(nameof(GetDeliveryEvent), new { deliveryEventId = createdVm.DeliveryEventId }, createdVm);
    }

    [HttpPut]
    [Route("{deliveryEventId:int}")]
    [ProducesResponseType(typeof(DeliveryEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryEventDto>> PutDeliveryEvent([FromRoute] int deliveryEventId, [FromBody] UpdateDeliveryEventRequestDto request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var existing = await deliveryEventService.GetDeliveryEventAsync(deliveryEventId);
        if (existing is null)
        {
            return NotFound();
        }

        existing.DeliveryEventDesc = request.DeliveryEventDesc;
        existing.Timestamp = request.Timestamp;
        existing.DeliveryId = request.DeliveryId;
        existing.CheckpointId = request.CheckpointId;
        existing.StatusId = request.StatusId;

        var updated = await deliveryEventService.UpdateDeliveryEventAsync(existing);
        return Ok(new DeliveryEventDto(
            updated.DeliveryEventId,
            updated.DeliveryEventDesc,
            updated.Timestamp,
            updated.DeliveryId,
            updated.CheckpointId,
            updated.Checkpoint?.CheckpointName,
            updated.StatusId,
            updated.Status?.StatusName));
    }

    [HttpDelete]
    [Route("{deliveryEventId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteDeliveryEvent([FromRoute] int deliveryEventId)
    {
        await deliveryEventService.DeleteDeliveryEventAsync(deliveryEventId);
        return NoContent();
    }
}
