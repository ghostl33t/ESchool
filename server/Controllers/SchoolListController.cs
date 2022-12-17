using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Other;
using server.Repositories.Interfaces;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolListController : Controller
    {
        private readonly Repositories.Interfaces.ISchoolList ISchoolList;
        private readonly ISchoolListValidations ISchoolListValidations;
        private readonly IFunctions functions;
        public SchoolListController(ISchoolList ISchoolList, ISchoolListValidations iSchoolListValidations, IFunctions functions)
        {
            this.ISchoolList = ISchoolList;
            this.ISchoolListValidations = iSchoolListValidations;
            this.functions = functions;
        }
        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var listOfSchools = await this.ISchoolList.GetSchoolsList();
            return Ok(listOfSchools);
        }
        [Authorize]
        [HttpGet]
        [Route("get-school/{Id}")]
        public async Task<IActionResult> GetSchoolAsync(long id)
        {
            return Ok(await ISchoolList.GetSchoolById(id));
        }
        [Authorize]
        [HttpPost]
        [Route("create-school")]
        public async Task<IActionResult> CreateUserAsync(Models.DTOs.SchoolList.Create newschool)
        {
            string message = await ISchoolListValidations.Validation(newschool);
            if (ISchoolListValidations.validationResult == true)
            {
                await ISchoolList.CreateSchoolAsync(newschool);
            }
            return await functions.Response(ISchoolListValidations.code, message);
        }
        [Authorize]
        [HttpPatch]
        [Route("update-school")]
        public async Task<IActionResult> UpdateUserAsync(Models.DTOs.SchoolList.Update school)
        {
            string message = await ISchoolListValidations.Validation(school);
            if (ISchoolListValidations.validationResult == true)
            {
                await ISchoolList.ModifySchoolAsync(school);
            }
            return await functions.Response(ISchoolListValidations.code, message);
        }
        [Authorize]
        [HttpPatch]
        [Route("delete-user/{SchoolId}/{AdministratorId}")]
        public async Task<IActionResult> DeleteUserAsync(long SchoolId,long AdministratorId)
        {
            string message = await ISchoolListValidations.Validation(SchoolId, AdministratorId);
            if (ISchoolListValidations.validationResult == true)
            {
                await ISchoolList.DeleteSchoolAsync(SchoolId,AdministratorId);
            }
            return await functions.Response(ISchoolListValidations.code, message);
        }
    }
}
