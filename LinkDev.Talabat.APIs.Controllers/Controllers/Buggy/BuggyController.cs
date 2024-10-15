using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : ApiControllerBase
    {

        [HttpGet("notfound")]    // Get: /api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));  // 404
        }

        [HttpGet("servererror")]    // Get: /api/buggy/servererror
        public IActionResult GetServererror()
        {
            throw new Exception();  //500
        }

        [HttpGet("badrequest")]    // Get: /api/buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));  // 400
        }

        [HttpGet("badrequest/{id}")]    // Get: /api/buggy/badrequest/five
        public IActionResult GetValidationError(int id)  // => 400
        {
            return Ok();
        }

        [HttpGet("unauthorized")]    // Get: /api/buggy/unauthorized
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponse(401));  // 401
        }

        [HttpGet("forbidden")]    // Get: /api/buggy/unauthorized
        public IActionResult GetForbiddenRequest()
        {
            return Forbid();  // 401
        }

        [Authorize]
        [HttpGet("authorized")]    // Get: /api/buggy/authorized
        public IActionResult GetAuthorizedRequest()
        {
            return Ok();  // 401
        }

    }
}
