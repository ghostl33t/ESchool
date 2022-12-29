using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.Subject;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjects _subjectRepo;
        private readonly ISubjectValidations _subjectValidations;
        private readonly IResponseService _functions;
        private readonly IMapper _mapper;
        public SubjectController(ISubjects subjectRepo, ISubjectValidations subjectValidations, IResponseService functions, IMapper mapper)
        {
            this._subjectRepo = subjectRepo;
            this._subjectValidations = subjectValidations;
            this._functions = functions;
            this._mapper = mapper;
        }
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subjects = await _subjectRepo.GetSubjectsList();
            if(subjects != null)
            {
                var subjectsDTO = _mapper.Map<List<GetSubject>>(subjects);
                return await _functions.Response(200,subjectsDTO);
            }
            return await _functions.Response(401,"Data not found");
        }
        [Route("get-by-id/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long Id )
        {
            var subject = await _subjectRepo.GetSubjectById(Id);
            if(subject != null)
            {
                var subjectDTO = _mapper.Map<GetSubject>(subject);
                return await _functions.Response(200, subjectDTO);
            }
            return await _functions.Response(401, "Data not found");
        }
        [Route("create-subject")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.DTOs.Subject.PostSubject newSubjectDto)
        {
            if(newSubjectDto != null)
            {
                if(await _subjectValidations.Validation(newSubjectDto) == true)
                {
                    var subject = _mapper.Map<Subject>(newSubjectDto);
                    await _subjectRepo.CreateSubjectAsync(subject);
                }
            }
            return await _functions.Response(_subjectValidations.code, _subjectValidations.validationMessage);
        }
        [Route("update-subject/{Id}")]
        [HttpPatch]
        public async Task<IActionResult> Update(long Id, Models.DTOs.Subject.PatchSubject subjectDto)
        {
            if (subjectDto != null)
            {
                if (await _subjectValidations.Validation(Id,subjectDto) == true)
                {
                    var subject = _mapper.Map<Subject>(subjectDto);
                    subject.Id = Id;
                    await _subjectRepo.ModifySubject(Id,subject);
                }
            }
            return await _functions.Response(_subjectValidations.code, _subjectValidations.validationMessage);

        }
        [HttpPatch]
        [Route("delete-subject/{SubjectId}/{AdministratorId}")]
        public async Task<IActionResult> Delete(long SubjectId, long AdministratorId)
        {
          if (await _subjectValidations.Validation(SubjectId, AdministratorId) == true)
          {
              await _subjectRepo.DeleteSubjectAsync(SubjectId,AdministratorId);
          }
            return await _functions.Response(_subjectValidations.code, _subjectValidations.validationMessage);
        }
}
}
