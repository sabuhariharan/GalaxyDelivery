using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace GalaxyDelivery.Controllers;

[ApiController]
[Route("v1/api/Deliveries")]
[Produces("application/json")]
public class DeliveryController(IDeliveryService deliveryService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeliveryListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeliveryListItemDto>>> GetDeliveries()
    {
        var deliveries = await deliveryService.GetDeliveriesAsync();
        var vms = deliveries.Select(d => new DeliveryListItemDto(
            d.DeliveryId,
            d.Origin,
            d.Destination,
            d.Driver?.DriverName,
            d.Vehicle?.VehicleMake,
            d.Route?.RouteName,
            (d.DeliveryEvent ?? Array.Empty<DeliveryEvent>()).Select(e => new DeliveryEventItemDto(
                e.DeliveryEventId,
                e.DeliveryEventDesc,
                e.Timestamp,
                e.DeliveryId,
                e.Checkpoint?.CheckpointName,
                e.Status?.StatusName))));

        return Ok(vms);
    }

    [HttpGet]
    [Route("{deliveryId:int}")]
    [ProducesResponseType(typeof(DeliveryDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryDetailDto>> GetDelivery([FromRoute] int deliveryId)
    {
        var delivery = await deliveryService.GetDeliveryAsync(deliveryId);
        if (delivery is null)
        {
            return NotFound();
        }

        var dto = new DeliveryDetailDto(
            delivery.DeliveryId,
            delivery.Origin,
            delivery.Destination,
            delivery.Driver?.DriverName,
            delivery.Vehicle?.VehicleMake,
            delivery.Route?.RouteName,
            (delivery.DeliveryEvent ?? Array.Empty<DeliveryEvent>()).Select(e => new DeliveryEventItemDto(
                e.DeliveryEventId,
                e.DeliveryEventDesc,
                e.Timestamp,
                e.DeliveryId,
                e.Checkpoint?.CheckpointName,
                e.Status?.StatusName)));

        return Ok(dto);
    }

    [HttpGet]
    [Route("{deliveryId:int}/summary")]
    [ProducesResponseType(typeof(DeliverySummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliverySummaryDto>> GetDeliverySummary([FromRoute] int deliveryId)
    {
        var delivery = await deliveryService.GetDeliveryAsync(deliveryId);
        if (delivery is null)
        {
            return NotFound();
        }

        var orderedEvents = (delivery.DeliveryEvent ?? Array.Empty<DeliveryEvent>())
            .OrderBy(e => e.Timestamp)
            .ToList();

        var timeTaken = orderedEvents.Count >= 2
            ? orderedEvents[^1].Timestamp - orderedEvents[0].Timestamp
            : TimeSpan.Zero;

        var summary = new DeliverySummaryDto(
            deliveryId,
            delivery.Origin,
            delivery.Destination,
            delivery.Route?.RouteName,
            delivery.Driver?.DriverName,
            delivery.Vehicle?.VehicleMake,
            timeTaken,
            orderedEvents.Select(e => new DeliverySummaryEventItemDto(
                e.DeliveryEventDesc,
                e.Timestamp,
                e.Checkpoint?.CheckpointName,
                e.Status?.StatusName)));

        return Ok(summary);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeliveryDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeliveryDetailDto>> PostDelivery([FromBody] CreateDeliveryRequestDto request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var delivery = new Delivery
        {
            Origin = request.Origin,
            Destination = request.Destination,
            DriverId = request.DriverId,
            VehicleId = request.VehicleId,
            RouteId = request.RouteId
        };

        var created = await deliveryService.CreateDeliveryAsync(delivery);

        // Re-read with includes resolved for Driver/Vehicle/Events
        var full = await deliveryService.GetDeliveryAsync(created.DeliveryId);
        var dto = new DeliveryDetailDto(
            full.DeliveryId,
            full.Origin,
            full.Destination,
            full.Driver?.DriverName,
            full.Vehicle?.VehicleMake,
            full.Route?.RouteName,
            (full.DeliveryEvent ?? Array.Empty<DeliveryEvent>()).Select(e => new DeliveryEventItemDto(
                e.DeliveryEventId,
                e.DeliveryEventDesc,
                e.Timestamp,
                e.DeliveryId,
                e.Checkpoint?.CheckpointName,
                e.Status?.StatusName)));

        return CreatedAtAction(nameof(GetDelivery), new { deliveryId = dto.DeliveryId }, dto);
    }

    [HttpPut]
    [Route("{deliveryId:int}")]
    [ProducesResponseType(typeof(DeliveryDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryDetailDto>> PutDelivery([FromRoute] int deliveryId, [FromBody] UpdateDeliveryRequestDto request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var existing = await deliveryService.GetDeliveryAsync(deliveryId);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Origin = request.Origin;
        existing.Destination = request.Destination;
        existing.DriverId = request.DriverId;
        existing.VehicleId = request.VehicleId;
        existing.RouteId = request.RouteId;

        var updated = await deliveryService.UpdateDeliveryAsync(existing);

        var full = await deliveryService.GetDeliveryAsync(updated.DeliveryId);
        var dto = new DeliveryDetailDto(
            full.DeliveryId,
            full.Origin,
            full.Destination,
            full.Driver?.DriverName,
            full.Vehicle?.VehicleMake,
            full.Route?.RouteName,
            (full.DeliveryEvent ?? Array.Empty<DeliveryEvent>()).Select(e => new DeliveryEventItemDto(
                e.DeliveryEventId,
                e.DeliveryEventDesc,
                e.Timestamp,
                e.DeliveryId,
                e.Checkpoint?.CheckpointName,
                e.Status?.StatusName)));

        return Ok(dto);
    }

    [HttpDelete]
    [Route("{deliveryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDelivery([FromRoute] int deliveryId)
    {
        var existing = await deliveryService.GetDeliveryAsync(deliveryId);
        if (existing is null)
        {
            return NotFound();
        }

        try
        {
            await deliveryService.DeleteDeliveryAsync(deliveryId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
