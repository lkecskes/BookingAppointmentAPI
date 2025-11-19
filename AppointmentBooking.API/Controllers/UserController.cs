using AppointmentBooking.Application.Services.Users;
using AppointmentBooking.Contracts.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace AppointmentBooking.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/profile")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        // GET api/profile/me
        [HttpGet("me")]
        [SwaggerOperation(OperationId = "GetMyProfile")]
        public async Task<ActionResult<ProfileResponse>> GetMyProfile()
        {
            var userId = GetCurrentUserId();
            var profile = await _userService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        // PUT api/profile/me
        [HttpPut("me")]
        [SwaggerOperation(OperationId = "UpdateMyProfile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = GetCurrentUserId();
            var success = await _userService.UpdateProfileAsync(userId, request);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // Admin only endpoints
        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}")]
        [SwaggerOperation(OperationId = "GetProfile")]
        public async Task<ActionResult<ProfileResponse>> GetProfile(int userId)
        {
            var profile = await _userService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}")]
        [SwaggerOperation(OperationId = "UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromBody] UpdateProfileRequest request)
        {
            var success = await _userService.UpdateProfileAsync(userId, request);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
