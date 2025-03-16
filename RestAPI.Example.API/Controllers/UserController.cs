using Microsoft.AspNetCore.Mvc;
using RestAPI.Example.API.Mapping;
using RestAPI.Example.Application.Services;
using RestAPI.Example.Contract.Request;

namespace RestAPI.Example.API.Controllers
{
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost(APIEndpoints.User.Login)]
        public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var response = await userService.Login(loginRequest, cancellationToken);
            return Ok(response);
        }

        [HttpPost(APIEndpoints.User.SignUp)]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest, CancellationToken cancellationToken)
        {

            var isCreated = await userService.SignUp(signUpRequest.MapToUser(), cancellationToken);

            if (isCreated) return Ok("User is created");

            return StatusCode(500, "Something went wrong please after sometime");
        }
    }
}
