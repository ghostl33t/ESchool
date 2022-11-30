using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly Repositories.Interfaces.IUser IUser;
        private readonly Validations.IUserValidations IUserValidations;
        public UserController(Repositories.Interfaces.IUser _IUser, Validations.IUserValidations iUserValidations)
        {
            this.IUser = _IUser;
            this.IUserValidations = iUserValidations;
        }
        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var listOfUsers = await this.IUser.GetAllAsync();
            return Ok(listOfUsers);
        }
        [Authorize]
        [HttpGet]
        [Route("get-user/{Id}")]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            return Ok(await IUser.GetUserAsync(id));
        }
        [Authorize]
        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUserAsync(Models.DTOs.UsersDTO.Create newUser)
        {
            string message = await IUserValidations.CheckUserOnCreation(newUser);
            if(IUserValidations.Validated == false)
            {
                return BadRequest(message);
            }
            return Ok(await IUser.CreateUserAsync(newUser));
        }
        [Authorize]
        [HttpPatch]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUserAsync(Models.DTOs.UsersDTO.Update user)
        {
            return Ok(await IUser.UpdateUserAsync(user));
        }
        [Authorize]
        [HttpPatch]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUserAsync(Models.DTOs.UsersDTO.Delete user)
        {
            return Ok(await IUser.DeleteUserAsync(user));
        }
    }
}
