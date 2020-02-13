using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo )
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult>(string username, string password)
        {
            //validate request
            username = username.toLower();
            if (await _repo.userExists(username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                Username = username;
            }

            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);
        }
        
    }
}