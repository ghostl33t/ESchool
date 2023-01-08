using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.UsersDTO;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly Repositories.Interfaces.IUser _userrepo;
        private readonly IUserValidations _userValidations;
        private readonly IResponseService _functions;
        private readonly IMapper _mapper;
        public UserController(IUser userrepo, IUserValidations userValidations, IResponseService functions, IMapper mapper)
        {
            this._userrepo = userrepo;
            this._userValidations = userValidations;
            this._functions = functions;
            this._mapper = mapper;
        }
        
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var listOfUsers = await _userrepo.GetAllAsync();
            
            if(listOfUsers != null)
            {
                var listOfUsersDTO = _mapper.Map<List<GetUser>>(listOfUsers);
                return await _functions.Response(200, listOfUsersDTO);
            }
            return await _functions.Response(400, "Users not found");
        }
        [HttpGet]
        [Route("get-user/{Id}")]
        public async Task<IActionResult> GetUserAsync(long id)
        {
           var user = await _userrepo.GetUserAsync(id);
           if(user != null)
            {
                var userdto = _mapper.Map<GetUser>(user);
                return await _functions.Response(200,userdto);
            }
            return await _functions.Response(400, "User not found");
        }
        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUserAsync(PostUser newUser)
        {

            if(await _userValidations.Validate(newUser) == true)
            {
                var user = _mapper.Map<User>(newUser);
                await _userrepo.CreateUserAsync(user);
            }
            return await _functions.Response(_userValidations.code, _userValidations.validationMessage);
        }
        [HttpPatch]
        [Route("update-user/{Id}")]
        public async Task<IActionResult> UpdateUserAsync(long Id, PatchUser userDto)
        {
            if (await _userValidations.Validate(Id,userDto) == true)
            {
                var user = _mapper.Map<User>(userDto);
                await _userrepo.UpdateUserAsync(Id,user);
            }
            return await _functions.Response(_userValidations.code, _userValidations.validationMessage);
        }
        [HttpPatch]
        [Route("delete-user/{userId}/{administratorId}")]
        public async Task<IActionResult> DeleteUserAsync(long userId, long administratorId)
        {
            if (await _userValidations.Validate(userId, administratorId) == true)
            {
                await _userrepo.DeleteUserAsync(userId,administratorId);
            }
            return await _functions.Response(_userValidations.code, _userValidations.validationMessage);
        }
    }
}
