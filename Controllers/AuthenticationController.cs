using internapi.Model;
using internapi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace internapi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;

        public AuthenticationController(IAuthenticationRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Authentication auth)
        {
            var user = _authRepo.Authenticate(auth.Mail, auth.Password);
            if (user is null)
                return BadRequest(new { message = "Username or password is wrong!" });
            return Ok(user);
        }
    }
}