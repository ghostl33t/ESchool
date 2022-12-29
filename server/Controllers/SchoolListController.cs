using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SchoolListController : Controller
    {
        private readonly Repositories.Interfaces.ISchoolList _schoolListRepo;
        private readonly ISchoolListValidations _schoolListValidation;
        private readonly IResponseService _functions;
        private readonly IMapper _mapper;
        public SchoolListController(ISchoolList ISchoolList, ISchoolListValidations iSchoolListValidations, IResponseService functions, IMapper mapper)
        {
            this._schoolListRepo = ISchoolList;
            this._schoolListValidation = iSchoolListValidations;
            this._functions = functions;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var listOfSchools = await this._schoolListRepo.GetSchoolsList();
            return Ok(listOfSchools);
        }
        [HttpGet]
        [Route("get-school/{Id}")]
        public async Task<IActionResult> GetSchoolAsync(long id)
        {
            return Ok(await _schoolListRepo.GetSchoolById(id));
        }
        [HttpPost]
        [Route("create-school")]
        public async Task<IActionResult> CreateUserAsync(Models.DTOs.SchoolList.PostSchoolList newschoolDTO)
        {
           
            if (await _schoolListValidation.Validation(newschoolDTO) == true)
            {
                var newschool = _mapper.Map<SchoolList>(newschoolDTO);
                await _schoolListRepo.CreateSchoolAsync(newschool);
            }
            return await _functions.Response(_schoolListValidation.code, _schoolListValidation.validationMessage);
        }
        [HttpPatch]
        [Route("update-school/{Id}")]
        public async Task<IActionResult> UpdateUserAsync(long Id,Models.DTOs.SchoolList.PatchUpdate schoolDto)
        {
            if (await _schoolListValidation.Validation(Id, schoolDto) == true)
            {
                var school = _mapper.Map<SchoolList>(schoolDto);
                await _schoolListRepo.ModifySchoolAsync(Id,school);
            }
            return await _functions.Response(_schoolListValidation.code, _schoolListValidation.validationMessage);
        }
        [HttpPatch]
        [Route("delete-school/{SchoolId}/{AdministratorId}")]
        public async Task<IActionResult> DeleteUserAsync(long SchoolId,long AdministratorId)
        {
            if(await _schoolListValidation.Validation(SchoolId, AdministratorId) == true)
            {
                await _schoolListRepo.DeleteSchoolAsync(SchoolId, AdministratorId);
            }
            return await _functions.Response(_schoolListValidation.code, _schoolListValidation.validationMessage);
        }
    }
}
