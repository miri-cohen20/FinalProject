using Bl.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("history")]
        public IActionResult GetHistory([FromBody] int id)
        {
            return  Ok(_blRenting.GetAllMyRenting(id));
        }

        

    }
}
