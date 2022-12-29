using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;
using AutoMapper;
using server.Models.DTOs.StudentDetails;
using server.Models.Domain;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("studentdetails")]
    public class StudentDetailsController : Controller
    {
        private readonly IStudentDetails _studentDetailsRepo;
        private readonly IMapper _mapper;
        public StudentDetailsController(IStudentDetails studentDetailsRepo, IMapper mapper)
        {
            _studentDetailsRepo = studentDetailsRepo;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("add-student-details")]
        public async Task<IActionResult> CreateStudentDetails(PostStudentDetails newstudentdetails)
        {
            var studentdetails = _mapper.Map<StudentDetails>(newstudentdetails);
            await _studentDetailsRepo.CreateStudentDetails(studentdetails);
            return Ok(studentdetails);
        }
    }
}
