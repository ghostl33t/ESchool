using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.ProfessorSubjects;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("professorsubjects")]

    public class ProfessorSubjectsController : Controller
    {
        private readonly IProfessorSubjectsRepository _professorSubjectRepository;
        private readonly IProfessorSubjectsValidation _professorSubjectValidations;
        private readonly IMapper _mapper;
        private readonly IResponseService _clFunctions;
        public ProfessorSubjectsController(IProfessorSubjectsRepository professorSubjectsRepository, IProfessorSubjectsValidation professorSubjectsValidation, IMapper mapper, IResponseService clFunctions)
        {
            _professorSubjectRepository = professorSubjectsRepository;
            _professorSubjectValidations = professorSubjectsValidation;
            _mapper = mapper;
            _clFunctions = clFunctions;
        }
        //Get
        [HttpGet]
        public async Task<IActionResult> GetProfessorsAndSubjects()
        {
            var data = await _professorSubjectRepository.GetProfessorsAndSubjects();
            if(data != null)
            {
                return await _clFunctions.Response(200, data);
            }
            return await _clFunctions.Response(400, "data not found");
        }
        //Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProfessorSubjectAsync(PostProfessorSubjects newProfSubjDto)
        {
            if(await _professorSubjectValidations.Validate(newProfSubjDto) == true)
            {
                if(newProfSubjDto != null)
                {
                    var newProfSubj = _mapper.Map<ProfessorSubjects>(newProfSubjDto);
                    await _professorSubjectRepository.CreateProfSubj(newProfSubj);
                }
            }
            return await _clFunctions.Response(_professorSubjectValidations.code, _professorSubjectValidations.validationMessage);
        }
        //Patch
        //Patch(delete)

    }
}
