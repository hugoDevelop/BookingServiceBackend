using BookingServiceBackend.Attributes;
using BookingServiceBackend.Models;
using BookingServiceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceBackend.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Anonimo]
        [HttpGet("v1/userSettings/{email}")]
        public async Task<ActionResult<User>> GetUserInformationByEmail(string email)
        {
            var user = await _userService.GetUserInformationByEmail(email);

            if (user == null)
            {
                return NotFound("El usuario no existe en la base de datos");
            }

            return user;
        }
    }
}
