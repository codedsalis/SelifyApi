using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SelifyApi.Interfaces;
using SelifyApi.Requests;
using SelifyApi.Responses;

namespace SelifyApi.Controllers;

[Route("api/v1/auth")]
public class AuthenticationController(IAuthService authService) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var isRegistered = await authService.Register(request);

        var response = new AuthResponse{
            Status = isRegistered ? "success" : "failed",
            Message = isRegistered ? "Resgistration successful" : "Something went wrong, please try again"
        };

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var token = await authService.Login(request.Email, request.Password);

        var authResponse = new AuthResponse
        {
            Status = "Success",
            Message = "Login Successful",
            Tokens = new Tokens()
            {
                // RefreshToken = "",
                AccessToken = token,
            }
        };

        return authResponse;
    }

    // [HttpPost("refresh-token")]
    // public Task<ActionResult> refreshToken()
    // {
    //     var token = await _authService.RefreshToken();
    // }
    // {

    // }
}