using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly ILogin ILogin;
        private readonly ITokenHandler ITokenHandler;
        public LoginController(ILogin ILogin, ITokenHandler ITokenHandler)
        {
            this.ILogin = ILogin;
            this.ITokenHandler = ITokenHandler;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Models.DTOs.UsersDTO.Login user)
        {
            var validUser = await ILogin.Login(user);
            if(validUser != null)
            {
                return Ok(await ITokenHandler.CreateTokenAsync(validUser));
            }
            else
            {
                return BadRequest("Unable to valid user!");
            }
        }

    }
}
