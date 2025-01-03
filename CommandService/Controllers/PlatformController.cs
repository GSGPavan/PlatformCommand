using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        public PlatformController()
        {
            
        }

        [HttpPost]
        public ActionResult<string> TestInBoundConnection()
        {
            Console.WriteLine("Testing inbound connection to command service");

            return Ok("Testing inbound connection to command service");
        }

    }
}
