using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        public DashboardController()
        {

        }

        // GET api/dashboard/home
        [HttpGet("home")]
        public IActionResult GetHome()
        {
            return new OkObjectResult(new { Message = "This is secure data!" });

        }

        [Authorize(Policy = "Admin")]
        [HttpGet("homeadmin")]
        public IActionResult GetNotHome()
        {
            return new OkObjectResult(new { Message = "This is secure admin data!" });

        }
    }

}
