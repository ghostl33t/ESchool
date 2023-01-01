using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using server.Models.Domain;
using server.Models.DTOs.ClassSubjects;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("classsubjects")]
    public class ClassSubjectsController : Controller
    {
        private readonly IClassSubjects _classSubjectsRepo;
        private readonly  IMapper _mapper;
        private readonly IResponseService _clFunctions;
        private readonly IClassSubjectsValidations _classSubjectsValidations;
        public ClassSubjectsController(IClassSubjects classSubjectsRepo, IMapper mapper, IResponseService clFunctions, IClassSubjectsValidations classsubjectValidations)
        {
            _classSubjectsRepo = classSubjectsRepo;
            _mapper = mapper;
            _clFunctions = clFunctions;
            _classSubjectsValidations = classsubjectValidations;
        }

        //Get
        [HttpGet]
        [Route("{classDepartmentId}")]
        public async Task<IActionResult> GetSubjectPerClass(long classDepartmentId)
        {
            var res = await _classSubjectsRepo.GetSubjectsPerClass(classDepartmentId);
            if(res == null)
            {
                return await _clFunctions.Response(400, "No data found");
            }
            return await _clFunctions.Response(200, res);
        }
        //post
        [HttpPost]
        [Route("create-class-subject")]
        public async Task<IActionResult> CreateClassSubject(PostClassSubjects classSubjectDto)
        {
            if(await _classSubjectsValidations.Validate(classSubjectDto) == true)
            {
                var classSubject = _mapper.Map<ClassSubjects>(classSubjectDto);
                await _classSubjectsRepo.CreateClassSubjects(classSubject);
            }
            return await _clFunctions.Response(_classSubjectsValidations.code, _classSubjectsValidations.validationMessage);
        }
        //patch
        [HttpPatch]
        [Route("update-class-subject/{Id}")]
        public async Task<IActionResult> UpdateClassSubject(long Id, PatchClassSubjects classSubjectdto)
        {
            if (await _classSubjectsValidations.Validate(Id, classSubjectdto) == true)
            {
                var classSubject = _mapper.Map<ClassSubjects>(classSubjectdto);
                await _classSubjectsRepo.UpdateClassSubjects(Id,classSubject);
            }
            return await _clFunctions.Response(_classSubjectsValidations.code, _classSubjectsValidations.validationMessage);
        }
        //delete-patch
        [HttpPatch]
        [Route("delete-class-subject/{Id}/{classLeaderId}")]
        public async Task<IActionResult> DeleteClassSubject(long Id, long classLeaderId)
        {
            if (await _classSubjectsValidations.Validate(Id, classLeaderId) == true)
            {
                await _classSubjectsRepo.DeleteClassSubjects(Id, classLeaderId);
            }
            return await _clFunctions.Response(_classSubjectsValidations.code, _classSubjectsValidations.validationMessage);
        }
    }
}
