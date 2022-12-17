using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Other;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly Repositories.Interfaces.IUser userrepo;
        private readonly IUserValidations userValidations;
        private readonly IFunctions functions;
        public UserController(Repositories.Interfaces.IUser _IUser, IUserValidations iUserValidations, IFunctions functions)
        {
            this.userrepo = _IUser;
            this.userValidations = iUserValidations;
            this.functions = functions;
        }
        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var listOfUsers = await this.userrepo.GetAllAsync();
            return Ok(listOfUsers);
        }
        [Authorize]
        [HttpGet]
        [Route("get-user/{Id}")]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            return Ok(await userrepo.GetUserAsync(id));
        }
        [Authorize]
        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUserAsync(Models.DTOs.UsersDTO.Create newUser)
        {
            string message = await userValidations.Validate(newUser);
            if(userValidations.Validated == true)
            {
                await userrepo.CreateUserAsync(newUser);
                return await functions.Response(userValidations.code, newUser);
            }
            return await functions.Response(userValidations.code, message);
        }
        [Authorize]
        [HttpPatch]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUserAsync(Models.DTOs.UsersDTO.Update user)
        {
            string message = await userValidations.Validate(user);
            if (userValidations.Validated == true)
            {
                await userrepo.UpdateUserAsync(user);
            }
            return await functions.Response(userValidations.code, message);
        }
        [Authorize]
        [HttpPatch]
        [Route("delete-user/{UserId}/{AdministratorId}")]
        public async Task<IActionResult> DeleteUserAsync(long UserId, long AdministratorId)
        {
            string message = await userValidations.Validate(UserId, AdministratorId);
            if (userValidations.Validated == true)
            {
                await userrepo.DeleteUserAsync(UserId,AdministratorId);
            }
            return await functions.Response(userValidations.code, message);
            
        }
    }
}
