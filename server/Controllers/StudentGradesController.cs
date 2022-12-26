using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;
using server.Validations;
namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentGradesController : Controller
    {
        private readonly IStudentGrades studentGrades;
        private readonly IStudentGradesValidations studentgradesvalidations;

        public StudentGradesController(IStudentGrades studentGrades, IStudentGradesValidations studentgradesvalidations )
        {
            this.studentGrades = studentGrades;
            this.studentgradesvalidations = studentgradesvalidations;
        }

        [HttpGet]
        [Route("get-student-grades/{Id}")]
        public async Task<IActionResult> GetStudentGradesAsync(long Id)
        {
            var grades = await studentGrades.GetGradesForStudent(Id);
            return Ok(grades);
        }
        [HttpPost]
        [Route("add-student-grade")]
        public async Task<IActionResult> CreateGradeAsync(server.Models.DTOs.StudentGrades.PostStudentGrades create)
        {
            await studentGrades.CreateGradeAsync(create);
            return Ok();
        }
    }
}
