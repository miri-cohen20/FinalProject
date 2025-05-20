using Bl.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal.models;
using YourNamespace;
using Bl.CustomerInput;

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
        public IActionResult GetHistoryCustomerRenting([FromQuery] string idCustomer)
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
        public IActionResult UpdateCustomer([FromBody] CustomerRegistration customer, string id)
        {

            try
            {
                
                return Ok(CustomerRegistration.FromCustomer(_blRenting.UpdateCustomer(customer.CreateCustomer(), id)));
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


        [HttpGet("getAllCar")]
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


        public class CarRentalRequest
        {
            public string IdCar { get; set; }
            public string IdCustomer { get; set; }
            public DateTime FromTime { get; set; }
            public DateTime ToTime { get; set; }
        }

        [HttpPost("renting")]
        public IActionResult RentingCar([FromBody] CarRentalRequest carRentalRequest)
        {
            try
            {
                if (_blRenting.RentingCar(carRentalRequest.IdCar, carRentalRequest.IdCustomer, carRentalRequest.FromTime, carRentalRequest.ToTime))
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
        public IActionResult GetAllMyCurrentRentals([FromQuery] string idCustomer)
        {
            try
            {
                List<Renting> allMyCurrentRentals = _blRenting.GetAllMyCurrentRentals(idCustomer);
               
                return Ok(CustomerRenting.FromRentingList(allMyCurrentRentals));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpGet("GetMyActiveAndFutureRentals")]
        public IActionResult GetMyActiveAndFutureRentals([FromQuery] string idCustomer)
        {
            try
            {
                List<Renting> allMyCurrentRentals = _blRenting.GetActiveAndFutureRentalsByCustomer(idCustomer);
                return Ok(CustomerRenting.FromRentingList(allMyCurrentRentals));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }





    }
}
