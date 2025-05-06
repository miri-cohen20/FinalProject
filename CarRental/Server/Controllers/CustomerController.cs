using Bl.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal.models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        IBlRenting _blRenting;
        public CustomerController(IBlRenting blRenting)
        {
            _blRenting=blRenting;
        }



        [HttpGet("getHistoryCustomerRenting")]
        public IActionResult GetHistoryCustomerRenting([FromBody] int idCustomer)
        {
            try
            {
                var allRenting = _blRenting.GetAllMyRenting(idCustomer);
                return Ok(allRenting);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }


        [HttpPut("updateCustomer")]
        public IActionResult UpdateCustomer( [FromBody] Customer customerDto)
        {

            try
            {
                _blRenting.UpdateCustomer(customerDto);
                return Ok("the customer update successfuly");
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


        [HttpGet("getAllIdCar")]
        public IActionResult GetAllIdCar()
        {
            try
            {
                var allIdCar = _blRenting.GetAllCars();
                return Ok(allIdCar);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpGet("getAllRenting")]    
        public IActionResult GetAllRenting()
        {
            try
            {
                var allRenting = _blRenting.GetAllRenting();
                return Ok(allRenting);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPut("renting")]
        public IActionResult RentingCar([FromBody] int idCar, int idCustomer, DateTime fromTime, DateTime toTime)
        {
            try
            {
                if (_blRenting.RentingCar(idCar, idCustomer, fromTime, toTime))
                    return Ok("the car rented successfuly");
                else
                    return BadRequest("the car not rented");
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

        [HttpGet("getAllMyCurrentRentals")]
        public IActionResult GetAllMyCurrentRentals([FromBody] int idCustomer)
        {
            try
            {
                var allMyCurrentRentals = _blRenting.GetAllMyCurrentRentals(idCustomer);
                return Ok(allMyCurrentRentals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
        




    }
}
