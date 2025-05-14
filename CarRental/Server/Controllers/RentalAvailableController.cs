using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bl.Api;   

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalAvailableController : ControllerBase
    {

        IBlRenting _blRenting;
        public RentalAvailableController(IBlRenting blRenting)
        {
            _blRenting = blRenting;
        }

        [HttpPut("extendRental")]
    public IActionResult ExtendRental([FromBody] int idRenting, [FromQuery] string customerId, [FromQuery] DateTime untilTime)
        {

            try
            {
                if (_blRenting.ExtendingRentalForACertainPeriodTime(idRenting, customerId, untilTime))
                    return Ok("The rental has been successfully extended.");
                else
                    return BadRequest("Failed to extend the rental.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }


        [HttpPut("GetPriceForExtendRental")]
        public IActionResult GetPriceForExtendRental([FromBody] int idRenting, [FromQuery] string customerId, [FromQuery] DateTime untilTime)
        {

            try
            {
                return Ok(_blRenting.PriceAddedRentalExtension(idRenting,customerId,untilTime));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpGet("getUntilCanRental")]
        public IActionResult actionResult([FromQuery] string idCustomer, [FromQuery] int idRenting)
        {
            try
            {
                var untilCanRental = _blRenting.GetUntilCanRental(idCustomer, idRenting);
                return Ok(untilCanRental);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
        [HttpPut("lackOfCleanliness")]
        public IActionResult LackOfCleanliness([FromQuery] int idRenting, [FromBody] string description)
        {
            try
            {
                _blRenting.LackOfCleanliness(idRenting, description);
                return Ok("The report has been successfully submitted.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
        [HttpPut("improperty")]
        public IActionResult Impropriety([FromQuery] int idRenting, [FromBody] string description)
        {
            try
            {
                _blRenting.Improperty(idRenting, description);
                return Ok("The report has been successfully submitted.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
        [HttpPut("endRental")]
        public IActionResult EndRental([FromQuery] string idCustomer, [FromQuery] int idRenting)
        {
            try
            {
                _blRenting.EndRental(idCustomer, idRenting);
                return Ok("The rental has been successfully ended.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
