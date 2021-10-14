using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/com/[Controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        [HttpPost]
        public ActionResult TestConnection()
        {
            return Ok("Access to Seerver");
        }
    }
}
