using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;
using server.Services.LoginService;

namespace server.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ITokenHandler _tokenHandler;
        public LoginController(ILoginService loginService, ITokenHandler tokenHandler)
        {
            this._loginService = loginService;
            this._tokenHandler = tokenHandler;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Models.DTOs.UsersDTO.Login user)
        {
            var validUser = await _loginService.Login(user);
            if(validUser != null)
            {
                return Ok(await _tokenHandler.CreateTokenAsync(validUser));
            }
            else
            {
                return BadRequest("Unable to valid user!");
            }
        }

    }
}
