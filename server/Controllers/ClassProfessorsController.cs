using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.ClassProfessors;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers;
[ApiController]
[Route("classprofessors")]
public class ClassProfessorsController : Controller
{
    private readonly IClassProfessor _classProfessorsRepo;
    private readonly IClassProfessorsValidations _classProfessorValidations;
    private readonly IMapper _mapper;
    private readonly IResponseService _responseService;
    public ClassProfessorsController(IClassProfessor classProfessorRepo, IClassProfessorsValidations classProfessorValidations, IMapper mapper, IResponseService responseService)
    {
        _classProfessorsRepo = classProfessorRepo;
        _classProfessorValidations = classProfessorValidations;
        _mapper = mapper;
        _responseService = responseService;
    }

    //[HttpGet]
    //public async Task<IActionResult> GetClassProfessor()
    //{
    //    return null;
    //}
    [HttpPost]
    [Route("create-class-professor")]
    public async Task<IActionResult> CreateProfessor(PostClassProfessors classProfessordto)
    {
        if(await _classProfessorValidations.Validate(classProfessordto) == true)
        {
            var classProfessor = _mapper.Map<ClassProfessors>(classProfessordto);
            var res = await _classProfessorsRepo.CreateClassProfessor(classProfessor);
        }
        return await _responseService.Response(_classProfessorValidations.code, _classProfessorValidations.validationMessage);
    }
    [HttpPatch]
    [Route("update-class-professor/{Id}")]
    public async Task<IActionResult> UpdateProfessor(long Id, PatchClassProfessors classProfessordto)
    {
        if (await _classProfessorValidations.Validate(Id,classProfessordto) == true)
        {
            var classProfessor = _mapper.Map<ClassProfessors>(classProfessordto);
            var res = await _classProfessorsRepo.UpdateClassProfessor(Id,classProfessor);
        }
        return await _responseService.Response(_classProfessorValidations.code, _classProfessorValidations.validationMessage);
    }
    [HttpPatch]
    [Route("delete-class-professor/{Id}/{LeaderId}")]
    public async Task<IActionResult> UpdateProfessor(long Id, long LeaderId)
    {
        if (await _classProfessorValidations.Validate(Id, LeaderId) == true)
        {
            var res = await _classProfessorsRepo.DeleteClassProfessor(Id, LeaderId);
        }
        return await _responseService.Response(_classProfessorValidations.code, _classProfessorValidations.validationMessage);
    }
}
