using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;
using server.Validations;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolListController : Controller
    {
        private readonly Repositories.Interfaces.ISchoolList ISchoolList;
        private readonly Validations.ISchoolListValidations ISchoolListValidations;
        public SchoolListController(ISchoolList ISchoolList, Validations.ISchoolListValidations iSchoolListValidations)
        {
            this.ISchoolList = ISchoolList;
            this.ISchoolListValidations = iSchoolListValidations;
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
            if (ISchoolListValidations.validationResult == false)
            {
                return BadRequest(message);
            }
            return Ok(await ISchoolList.CreateSchoolAsync(newschool));
        }
        [Authorize]
        [HttpPatch]
        [Route("update-school")]
        public async Task<IActionResult> UpdateUserAsync(Models.DTOs.SchoolList.Update school)
        {
            return Ok(await ISchoolList.ModifySchoolAsync(school));
        }
        [Authorize]
        [HttpPatch]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUserAsync(long Id)
        {
            return Ok(await ISchoolList.DeleteSchoolAsync(Id));
        }
    }
}
