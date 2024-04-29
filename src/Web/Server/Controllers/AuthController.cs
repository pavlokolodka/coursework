using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;
using System.Text.Json.Serialization;
using Web.Shared.Dto;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        public AuthController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        
        [HttpPost("register")]
        public ActionResult<JWTResponse> Register([FromBody] CreateUserDto payload)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            var user = _userService.FindOneByEmail(payload.Email);

            if (user != null) {
                return Conflict("User with this email already exists");
            }

            var createdUser = _userService.Create(payload);
            
            // TODO: Add email verification
            _userService.VerifyUser(createdUser.ID.ToString());
            
            string jwt = _authService.GenerateJwtToken(createdUser.ID, createdUser.Email);

            return Ok(new JWTResponse() { AcessToken = jwt });
        }

        [HttpPost("login")]
        public ActionResult<JWTResponse> Login([FromBody] LoginDto payload)
        {
           /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            var userEmail = payload.Email;
            var userPassword = payload.Password;
            var user = _userService.FindUserAndComparePassword(userEmail, userPassword);

            if (user == null)
            {
                return UnprocessableEntity("Wrong email or password");
            }

            string jwt = _authService.GenerateJwtToken(user.ID, user.Email);

            return Ok(new JWTResponse() { AcessToken = jwt });
        }
    }
    public class JWTResponse
    {
        [JsonPropertyName("access_token")]
        public string AcessToken { get; set; }
    }
}
