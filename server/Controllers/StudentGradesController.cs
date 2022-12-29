using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.StudentGrades;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;
using System.Runtime.InteropServices;

namespace server.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class StudentGradesController : Controller
{
    private readonly IStudentGrades _studentGrades;
    private readonly IStudentGradesValidations _studentgradesvalidations;
    private readonly IResponseService _clfunctions;
    private readonly IMapper _mapper;
    public StudentGradesController(IStudentGrades studentGrades, IStudentGradesValidations studentgradesvalidations, IResponseService clfunctions, IMapper mapper)
    {
        _studentGrades = studentGrades;
        _studentgradesvalidations = studentgradesvalidations;
        _clfunctions = clfunctions;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get-student-grades/{Id}")]
    public async Task<IActionResult> GetStudentGradesAsync(long Id)
    {
        var grades = await _studentGrades.GetGradesForStudent(Id);
        if(grades != null)
        {
            return await _clfunctions.Response(200, grades);
        }
        return await _clfunctions.Response(400, "Grades not found");
    }
    [HttpPost]
    [Route("add-student-grade")]
    public async Task<IActionResult> CreateGradeAsync(PostStudentGrades grade)
    {
        if(await _studentgradesvalidations.Validations(grade) == true)
        {
            var newGrade = _mapper.Map<StudentGrades>(grade);
            await _studentGrades.CreateGradeAsync(newGrade);
        }
        return await _clfunctions.Response(_studentgradesvalidations.code, _studentgradesvalidations.validationMessage);
    }
    [HttpPatch]
    [Route("modify-student-grade/{Id}")]
    public async Task<IActionResult> UpdateGradeAsync(long Id, PatchStudentGrades grade)
    {
        if (await _studentgradesvalidations.Validations(Id,grade) == true)
        {
            var newGrade = _mapper.Map<StudentGrades>(grade);
            await _studentGrades.UpdateGradeAsync(Id,newGrade);
        }
        return await _clfunctions.Response(_studentgradesvalidations.code, _studentgradesvalidations.validationMessage);
    }
    [HttpPatch]
    [Route("delete-student-grade/{gradeId}/{professorId}")]
    public async Task<IActionResult> DeleteGradeAsync(long gradeId, long professorId)
    {
        if (await _studentgradesvalidations.Validations(gradeId, professorId) == true)
        {
            await _studentGrades.DeleteGradeAsync(gradeId, professorId);
        }
        return await _clfunctions.Response(_studentgradesvalidations.code, _studentgradesvalidations.validationMessage);
    }
}
