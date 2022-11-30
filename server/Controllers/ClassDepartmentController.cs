using Microsoft.AspNetCore.Mvc;
using server.Database;
using server.Repositories.Interfaces;
using server.Validations;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassDepartmentController : Controller
    {
        private readonly IClassDepartment IClassDepartment;
        private readonly IClassDepartmentValidations IClassDepartmentValidations;
        public ClassDepartmentController(IClassDepartment IClassDepartment, IClassDepartmentValidations IClassDepartmentValidations)
        {
            this.IClassDepartment = IClassDepartment;
            this.IClassDepartmentValidations = IClassDepartmentValidations;
        }
        //TODO napravit kontroler
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //if(this.IClassDepartmentValidations.Validation())
            var classList = await IClassDepartment.GetSchoolsList();
            if(classList != null)
            {
                return Ok(classList);
            }
            return BadRequest();
        }
        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long Id )
        {
            var classL = await IClassDepartment.GetSchoolById(Id);
            if(classL != null)
            {
                return Ok(classL);
            }
            return BadRequest();
        }
        [Route("create-department")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.DTOs.ClassDepartment.Create newClass)
        {

            await IClassDepartmentValidations.Validation(newClass);
            if (IClassDepartmentValidations.validationResult == true)
            {
                return Ok(IClassDepartment.CreateSchoolAsync(newClass));
            }
            else
            {
                return BadRequest();
            }     
        }
        [Route("update-department")]
        [HttpPatch]
        public async Task<IActionResult> Update(Models.DTOs.ClassDepartment.Update classdep)
        {
            return Ok(await IClassDepartment.ModifySchoolAsync(classdep));

        }
        [Route("delete-department")]
        [HttpPatch]
        public async Task<IActionResult> Delete(long Id)
        {
            return Ok(await IClassDepartment.DeleteSchoolAsync(Id));
        }
}
}
