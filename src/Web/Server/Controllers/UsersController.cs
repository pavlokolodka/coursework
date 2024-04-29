using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Web.Shared.Dto;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly BookingService _bookingService;
        private readonly AuthService _authService;
        public UsersController(UserService userService, AuthService authService, BookingService bookingService) {
            _userService = userService;
            _authService = authService;
            _bookingService = bookingService;
        }
   
        [HttpGet]
        [Authorize]
        public ActionResult<UserDto> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _userService.FindOneById(userId); 
            
            if (user == null || user.IsAdmin == false) { 
                return Forbid();
            }

            var users = _userService.Find(user => true);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<UserDto> Get(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _userService.FindOneById(userId);

            if (user == null || user.IsAdmin == false && !userId.Equals(id))
            {
                return Forbid();
            }

            var foundUser = _userService.FindOneById(id);

            if (foundUser == null)
            {
                return NotFound();
            }

            return Ok(foundUser);
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<UserDto> Me()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _userService.FindOneById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<UserDto> Put(string id, [FromBody] UpdateUserDto payload)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
            if (userId == null)
            {
                return Unauthorized();
            }
            
            if (!userId.Equals(id))
            {
                return Forbid();
            }

            var updatedUser = _userService.Update(id, payload);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _userService.FindOneById(userId);

            if (user == null || user.IsAdmin == false && !userId.Equals(id))
            {
                return Forbid();
            }

            var userBookings = _bookingService.FindAll(userId);

            if (userBookings.Count > 0) {
                return Conflict("Cannot delete user with active bookings");
            }

            var updatedUser = _userService.Delete(id);

            if (!updatedUser)
            {
                return NotFound();
            }

            return NoContent();
        }
    }    
}
