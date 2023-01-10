using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using server.Models.DTOs.Login;
using server.Repositories.Interfaces;
using server.Services.LoginService;
using server.Services.ResponseService;

namespace server.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IResponseService _responseService;
        public LoginController(ILoginService loginService, ITokenHandler tokenHandler, IResponseService responseService)
        {
            this._loginService = loginService;
            this._tokenHandler = tokenHandler;
            this._responseService = responseService;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Models.DTOs.UsersDTO.Login user)
        {
            var validUser = await _loginService.Login(user);
            if(validUser != null)
            {
                LoginDTO loginResponse = new()
                {
                    Username = user.UserName,
                    Token = await _tokenHandler.CreateTokenAsync(validUser)
                };
                
                return await _responseService.Response(200,loginResponse);
            }
            else
            {
                return await _responseService.Response(401, "Unauthorized!");
            }
        }

    }
}
