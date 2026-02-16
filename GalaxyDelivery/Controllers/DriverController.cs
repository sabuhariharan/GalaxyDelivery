using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyDelivery.Controllers
{
    [ApiController]
    [Route("v1/api/Drivers")]
    [Produces("application/json")]
    public class DriverController(IDriverService driverService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DriverDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
        {
            var drivers = await driverService.GetDriversAsync();
            var vms = drivers.Select(d => new DriverDto(d.DriverId, d.DriverName));

            return Ok(vms);
        }

        [HttpGet]
        [Route("{driverId:int}")]
        [ProducesResponseType(typeof(DriverDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverDto>> GetDriver([FromRoute] int driverId)
        {
            var driver = await driverService.GetDriverAsync(driverId);
            if (driver is null)
            {
                return NotFound();
            }

            return Ok(new DriverDto(driver.DriverId, driver.DriverName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(DriverDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverDto>> PostDriver([FromBody] CreateDriverRequestDto request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var driver = new Driver
            {
                DriverName = request.DriverName
            };

            await driverService.CreateDriverAsync(driver);

            var createdVm = new DriverDto(driver.DriverId, driver.DriverName);

            return CreatedAtAction(nameof(GetDriver), new { driverId = createdVm.DriverId }, createdVm);
        }

        [HttpPut]
        [Route("{driverId:int}")]
        [ProducesResponseType(typeof(DriverDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverDto>> PutDriver([FromRoute] int driverId, [FromBody] UpdateDriverRequestDto request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var existing = await driverService.GetDriverAsync(driverId);
            if (existing is null)
            {
                return NotFound();
            }

            existing.DriverName = request.DriverName;

            var updated = await driverService.UpdateDriverAsync(existing);
            return Ok(new DriverDto(updated.DriverId, updated.DriverName));
        }

        [HttpDelete]
        [Route("{driverId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDriver([FromRoute] int driverId)
        {
            await driverService.DeleteDriverAsync(driverId);
            return NoContent();
        }

    }
}
