using Microsoft.AspNetCore.Mvc;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;
using server.Validations;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjects ISubject;
        private readonly ISubjectValidations ISubjectValidations;
        public SubjectController(ISubjects ISubject, ISubjectValidations ISubjectValidations)
        {
            this.ISubject = ISubject;
            this.ISubjectValidations = ISubjectValidations;
        }
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //if(this.IClassDepartmentValidations.Validation())
            var subjects = await ISubject.GetSubjectsList();
            if(subjects != null)
            {
                return Ok(subjects);
            }
            return BadRequest();
        }
        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long Id )
        {
            var subject = await ISubject.GetSubjectById(Id);
            if(subject != null)
            {
                return Ok(subject);
            }
            return BadRequest();
        }
        [Route("create-subject")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.DTOs.Subject.Create newSubject)
        {

            await ISubjectValidations.Validation(newSubject);
            if (ISubjectValidations.validationResult == true)
            {
                return Ok(ISubject.CreateSubjectAsync(newSubject));
            }
            else
            {
                return BadRequest();
            }     
        }
        [Route("update-subject")]
        [HttpPatch]
        public async Task<IActionResult> Update(Models.DTOs.Subject.Update subject)
        {
            return Ok(await ISubject.ModifySubject(subject));

        }
        [Route("delete-subject")]
        [HttpPatch]
        public async Task<IActionResult> Delete(long Id)
        {
            return Ok(await ISubject.DeleteSubjectAsync(Id));
        }
}
}
