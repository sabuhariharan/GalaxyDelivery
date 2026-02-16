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
    [Route("v1/api/Vehicles")]
    [Produces("application/json")]
    public class VehicleController(IVehicleService vehicleService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VehicleDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicles = await vehicleService.GetVehiclesAsync();
            var vms = vehicles.Select(v => new VehicleDto(v.VehicleId, v.VehicleMake));
            return Ok(vms);
        }

        [HttpGet]
        [Route("{vehicleId:int}")]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VehicleDto>> GetVehicle([FromRoute] int vehicleId)
        {
            var vehicle = await vehicleService.GetVehicleAsync(vehicleId);
            if (vehicle is null)
            {
                return NotFound();
            }

            return Ok(new VehicleDto(vehicle.VehicleId, vehicle.VehicleMake));
        }

        [HttpPost]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VehicleDto>> PostVehicle([FromBody] CreateVehicleRequestDto request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var vehicle = new Vehicle { VehicleMake = request.VehicleMake };
            var created = await vehicleService.CreateVehicleAsync(vehicle);

            var createdVm = new VehicleDto(created.VehicleId, created.VehicleMake);
            return CreatedAtAction(nameof(GetVehicle), new { vehicleId = createdVm.VehicleId }, createdVm);
        }

        [HttpPut]
        [Route("{vehicleId:int}")]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VehicleDto>> PutVehicle([FromRoute] int vehicleId, [FromBody] UpdateVehicleRequestDto request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var existing = await vehicleService.GetVehicleAsync(vehicleId);
            if (existing is null)
            {
                return NotFound();
            }

            existing.VehicleMake = request.VehicleMake;
            var updated = await vehicleService.UpdateVehicleAsync(existing);

            return Ok(new VehicleDto(updated.VehicleId, updated.VehicleMake));
        }

        [HttpDelete]
        [Route("{vehicleId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {
            await vehicleService.DeleteVehicleAsync(vehicleId);
            return NoContent();
        }
    }
}
