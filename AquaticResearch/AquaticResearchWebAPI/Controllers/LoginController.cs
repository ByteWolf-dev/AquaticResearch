using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticResearch.Controllers
{
    /// <summary>
    /// Login controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private readonly UserManager _userManager;

        /// <summary>
        /// Constructor for LoginController.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        public LoginController(IConfiguration configuration, UserManager userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// Login with username and password.
        /// </summary>
        /// <param name="login">Username, password, ...</param>
        /// <returns>The JWT with claims.</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            var user = _userManager.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = _userManager.GenerateJwt(user);
                response = Ok(new { accessToken = tokenString, username = login.Username });
            }

            return response;
        }
    }
}
