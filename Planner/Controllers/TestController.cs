using System;
using Microsoft.AspNetCore.Mvc;
using Planner.Authorization;

namespace Planner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [AllowAnonymous]
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("Not restricted !");
        }

        
        [HttpGet("treasure")]
        public IActionResult Treasure()
        {
            return Ok("It is restricted !");
        }
    }
}
