using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        [HttpPost()]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine($"--> Inbound POST # Command Service");
            
            return Ok("Inbound POST # Command Service");
        }
    }
}