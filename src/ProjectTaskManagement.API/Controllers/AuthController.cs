using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common;
using ProjectTaskManagement.Application.Dtos;
using ProjectTaskManagement.Application.Features.Auth;

namespace ProjectTaskManagement.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var response = await _sender.Send(command);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Registration successful."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _sender.Send(command);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Login successful."));
        }
    }
}
