using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.StudentGrades;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers;
[Authorize]
[ApiController]
[Route("studentgrades")]
public class StudentGradesController : Controller
{
    private readonly IStudentGrades _studentGradesRepo;
    private readonly IStudentGradesValidations _studentGradesValidation;
    private readonly IMapper _mapper;
    private readonly IResponseService _responseService;

    public StudentGradesController  (IStudentGrades studentGradesRepo, IStudentGradesValidations studentGradesValidation, IMapper mapper, IResponseService responseService)
    {
        _studentGradesRepo = studentGradesRepo;
        _studentGradesValidation = studentGradesValidation;
        _mapper = mapper;
        _responseService = responseService;
    }
    //get
     
    //post
    [HttpPost]
    [Route("create-grade")]
    public async Task<IActionResult> CreateGrade(PostStudentGrades gradeDto)
    {
        if(await _studentGradesValidation.Validate(gradeDto) == true)
        {
            var grade = _mapper.Map<StudentGrades>(gradeDto);
            await _studentGradesRepo.CreateGrade(grade);
        }
        return await _responseService.Response(_studentGradesValidation.code, _studentGradesValidation.validationMessage);
    }
    //patch
    [HttpPatch]
    [Route("update-grade")]
    public async Task<IActionResult> UpdateGrade(long Id, PatchStudentGrades gradeDto)
    {
        if (await _studentGradesValidation.Validate(Id, gradeDto) == true)
        {
            var grade = _mapper.Map<StudentGrades>(gradeDto);
            grade.Id = Id;
            await _studentGradesRepo.UpdateGrade(Id,grade);
        }
        return await _responseService.Response(_studentGradesValidation.code, _studentGradesValidation.validationMessage);
    }
    //Delete
    [HttpDelete]
    [Route("delete-grade/{Id}/{ProfessorId}")]
    public async Task<IActionResult> DeleteGrade(long Id, long ProfessorId)
    {
        if (await _studentGradesValidation.Validate(Id, ProfessorId) == true)
        {
            await _studentGradesRepo.DeleteGrade(Id, ProfessorId);
        }
        return await _responseService.Response(_studentGradesValidation.code, _studentGradesValidation.validationMessage);
    }
}
