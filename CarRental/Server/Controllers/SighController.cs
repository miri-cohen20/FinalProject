using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bl.Api;
using Microsoft.AspNetCore.Identity;
using Dal.models;
using YourNamespace;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SighController : ControllerBase
    {
        ISighUp _sighUp;
        ISighIn _sighIn;
        IBlRenting _blRenting;


        public SighController(ISighUp sighUp, ISighIn sighIn, IBlRenting blRenting)
        {
            _blRenting = blRenting;
            _sighUp = sighUp;
            _sighIn = sighIn;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Tuple<string, string> login)
        {
            try
            {
                var result = _sighIn.Log(login.Item1, login.Item2);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // לוג את השגיאה או טיפול בשגיאה לפי הצורך
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("isCostumer")]
        public IActionResult isCustomer([FromQuery] string id)
        {
            try
            {
                return Ok( _sighIn.IsCustomer(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("isWorker")]
        public IActionResult isWorker([FromQuery] string id)
        {
            try
            {
               return Ok( _sighIn.IsWorker(id));
                    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("sighUpCustomer")]
        public IActionResult sighUpCustomer([FromBody] CustomerRegistration customer)
        {
            try
            {
                // Validate the input
                if (customer == null)
                {
                    return BadRequest("Customer data is missing.");
                }

                // Attempt to create the customer
                Customer newCustomer = customer.CreateCustomer();
                bool registrationSuccessful = _sighUp.CreateCustomer(newCustomer);

                // Check if the registration was successful
                if (registrationSuccessful)
                {
                    return Ok(new
                    {
                        Message = "Registration successful.",
                        CustomerId = newCustomer.Id
                    });
                }

                // If registration failed due to user already existing
                return BadRequest("Registration failed. The user may already exist.");
            }
            catch (ArgumentException ex)
            {
                // Handle specific argument exceptions
                return BadRequest($"Invalid data: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        //[HttpPost("sighUpCustomer")]
        //public IActionResult sighUpCustomer([FromBody] CustomerRegistration custoner)
        //{
        //    if (_sighUp.CreateCustomer(custoner.CreateCustomer()))
        //        return Ok();
        //    return BadRequest("Registration failed.");

        //}

        //[HttpPost("sighUpCustomer")]
        //public IActionResult sighUpCustomer([FromBody] CustomerRegistration customer)
        //{
        //    var result = _sighUp.CreateCustomer(customer.CreateCustomer());
        //    if (result) // אם ההרשמה הצליחה
        //    {
        //        return Ok("Registration successful."); // החזר תגובה חיובית
        //    }
        //    return BadRequest("Registration failed."); // החזר הודעת שגיאה אם נכשלה
        //}

        //[HttpPost("sighUpWorkerById")]
        //public IActionResult sighUpWorkerById([FromBody] User user)
        //{
        //    if (_sighUp.CreateCustomer(user))
        //        return BadRequest("Registration failed.");
        //    return Ok();
        //}
        //[HttpPost("sighUpWorker")]
        //public IActionResult sighUpWorker([FromBody] Worker)
        //{
        //    if (_sighUp.CreateCustomer(user))
        //        return BadRequest("Registration failed.");
        //    return Ok();
        //}



    }
}
