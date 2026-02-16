using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GalaxyDelivery.Controllers
{
    [ApiController]
    [Route("v1/api/Routes")]
    [Produces("application/json")]
    public class RouteController(IRouteService routeService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeliveryRouteDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DeliveryRouteDto>>> GetRoutes()
        {
            var routes = await routeService.GetRoutesAsync();
            var vms = routes.Select(r => new DeliveryRouteDto(
                r.RouteId,
                r.RouteName,
                (r.Checkpoint ?? Enumerable.Empty<Checkpoint>()).Select(c => new CheckpointDto(c.CheckpointId, c.CheckpointName, r.RouteName))));

            return Ok(vms);
        }

        [HttpGet]
        [Route("{routeId:int}")]
        [ProducesResponseType(typeof(DeliveryRouteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeliveryRouteDto>> GetRoute([FromRoute] int routeId)
        {
            var route = await routeService.GetRouteAsync(routeId);
            if (route is null)
            {
                return NotFound();
            }

            return Ok(new DeliveryRouteDto(
                route.RouteId,
                route.RouteName,
                (route.Checkpoint ?? Enumerable.Empty<Checkpoint>()).Select(c => new CheckpointDto(c.CheckpointId, c.CheckpointName, route.RouteName))));
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeliveryRouteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeliveryRouteDto>> PostRoute([FromBody] CreateRouteRequestDto request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var route = new DeliveryRoute { RouteName = request.RouteName };
            var created = await routeService.CreateRouteAsync(route);
            var createdVm = new DeliveryRouteDto(created.RouteId, created.RouteName, Enumerable.Empty<CheckpointDto>());
            return CreatedAtAction(nameof(GetRoute), new { routeId = createdVm.RouteId }, createdVm);
        }

        [HttpPut]
        [Route("{routeId:int}")]
        [ProducesResponseType(typeof(DeliveryRouteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeliveryRouteDto>> PutRoute([FromRoute] int routeId, [FromBody] UpdateRouteRequestDto request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var existing = await routeService.GetRouteAsync(routeId);
            if (existing is null)
            {
                return NotFound();
            }

            existing.RouteName = request.RouteName;
            var updated = await routeService.UpdateRouteAsync(existing);

            var full = await routeService.GetRouteAsync(updated.RouteId);
            if (full is null)
            {
                return Ok(new DeliveryRouteDto(updated.RouteId, updated.RouteName, Enumerable.Empty<CheckpointDto>()));
            }

            return Ok(new DeliveryRouteDto(
                full.RouteId,
                full.RouteName,
                (full.Checkpoint ?? Enumerable.Empty<Checkpoint>()).Select(c => new CheckpointDto(c.CheckpointId, c.CheckpointName, full.RouteName))));
        }

        [HttpDelete]
        [Route("{routeId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRoute([FromRoute] int routeId)
        {
            await routeService.DeleteRouteAsync(routeId);
            return NoContent();
        }
    }
}
