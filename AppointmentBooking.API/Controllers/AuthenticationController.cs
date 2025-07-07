using AppointmentBooking.Api.Mappers;
using AppointmentBooking.Application.Services.Authentication;
using AppointmentBooking.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [SwaggerOperation(OperationId = "Register")]

    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(AuthenticationMapper.ToDto(request));

        var response = new RegisterResponse(
             result.Success,
             result.Token,
             result.ErrorMessage
         );

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    [SwaggerOperation(OperationId = "Login")]

    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(AuthenticationMapper.ToDto(request));

        var response = new LoginResponse(
                result.Success,
                result.Token,
                result.ErrorMessage
            );

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }
}
