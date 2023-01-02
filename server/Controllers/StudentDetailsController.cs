using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;
using AutoMapper;
using server.Models.DTOs.StudentDetails;
using server.Models.Domain;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("studentdetails")]
    public class StudentDetailsController : Controller
    {
        private readonly IStudentDetails _studentDetailsRepo;
        private readonly IMapper _mapper;
        private readonly IStudentDetailsValidations _studentDetailsValidation;
        private readonly IResponseService _clFunctions;
        public StudentDetailsController(IStudentDetails studentDetailsRepo, IMapper mapper, IStudentDetailsValidations studentDetailsValidation, IResponseService clFunctions)
        {
            _studentDetailsRepo = studentDetailsRepo;
            _mapper = mapper;
            _studentDetailsValidation = studentDetailsValidation;
            _clFunctions = clFunctions;
        }
        [HttpPost]
        [Route("add-student-details")]
        public async Task<IActionResult> CreateStudentDetails(PostStudentDetails newstudentdetailsdto)
        {
            if (await _studentDetailsValidation.Validate(newstudentdetailsdto) == true)
            {
                var studentdetails = _mapper.Map<StudentDetails>(newstudentdetailsdto);
                var res = await _studentDetailsRepo.CreateStudentDetails(studentdetails);
            }
            return await _clFunctions.Response(_studentDetailsValidation.code, _studentDetailsValidation.validationMessage);
        }
        [HttpPatch]
        [Route("update-student-details/{Id}")]
        public async Task<IActionResult> ModifyStudentDetails(long  Id, PatchStudentDetails studentDetailsDto)
        {
            if(await _studentDetailsValidation.Validate(Id, studentDetailsDto) == true)
            {
                var studentdetails = _mapper.Map<StudentDetails>(studentDetailsDto);
                studentdetails.Id = Id;
                var res = await _studentDetailsRepo.UpdateStudentDetails(Id, studentdetails);
            }
            return await _clFunctions.Response(_studentDetailsValidation.code, _studentDetailsValidation.validationMessage);
        }
        [HttpPatch]
        [Route("delete-student-details/{Id}/{AdministratorId}")]
        public async Task<IActionResult> ModifyStudentDetails(long Id, long AdministratorId)
        {
            if (await _studentDetailsValidation.Validate(Id, AdministratorId) == true)
            {
                var res = await _studentDetailsRepo.DeleteStudentDetails(Id, AdministratorId);
            }
            return await _clFunctions.Response(_studentDetailsValidation.code, _studentDetailsValidation.validationMessage);
        }
    }
}
