using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjects ISubject;
        private readonly ISubjectValidations subvalidations;
        private readonly IResponseService functions;
        public SubjectController(ISubjects ISubject, ISubjectValidations ISubjectValidations, IResponseService functions)
        {
            this.ISubject = ISubject;
            this.subvalidations = ISubjectValidations;
            this.functions = functions;
        }
        [Authorize]
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subjects = await ISubject.GetSubjectsList();
            if(subjects != null)
            {
                return Ok(subjects);
            }
            return BadRequest();
        }
        [Authorize]
        [Route("get-by-id/{Id}")]
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
        [Authorize]
        [Route("create-subject")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.DTOs.Subject.Create newSubject)
        {
            var message = await subvalidations.Validation(newSubject);
            if (subvalidations.validationResult == true)
            {
                await ISubject.CreateSubjectAsync(newSubject);
            }
            return await functions.Response(subvalidations.code, message);
        }
        [Authorize]
        [Route("update-subject")]
        [HttpPatch]
        public async Task<IActionResult> Update(Models.DTOs.Subject.Update subject)
        {
            var message = await subvalidations.Validation(subject);
            if (subvalidations.validationResult == true)
            {
                await ISubject.ModifySubject(subject);
            }
            return await functions.Response(subvalidations.code, message);

        }
        [Authorize]
        [HttpPatch]
        [Route("delete-subject/{SubjectId}/{AdministratorId}")]
        public async Task<IActionResult> Delete(long SubjectId, long AdministratorId)
        {
            var message = await subvalidations.Validation(SubjectId,AdministratorId);
            if (subvalidations.validationResult == true)
            {
                await ISubject.DeleteSubjectAsync(SubjectId,AdministratorId);
            }
            return await functions.Response(subvalidations.code, message);
        }
}
}
