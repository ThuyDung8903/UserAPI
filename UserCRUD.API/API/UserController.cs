using Microsoft.AspNetCore.Mvc;
using UserCRUD.API.Service;

namespace UserCRUD.API.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromQuery] string fullName, [FromQuery] List<string> addresses, [FromQuery] List<string> phoneNumbers)
        {
            try
            {
                if (string.IsNullOrEmpty(fullName) || addresses == null || phoneNumbers == null)
                {
                    return BadRequest("Null data");
                }

                var createId = _userService.CreateUser(fullName, addresses, phoneNumbers);
                return CreatedAtAction(nameof(GetUserById), new { id = createId }, createId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromQuery] string fullName, [FromQuery] List<string> addresses, [FromQuery] List<string> phoneNumbers)
        {
            try
            {
                if (string.IsNullOrEmpty(fullName) || addresses == null || phoneNumbers == null)
                {
                    return BadRequest("Null data");
                }

                _userService.UpdateUser(id, fullName, addresses, phoneNumbers);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
