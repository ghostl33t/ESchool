using AutoMapper;
namespace server.Profiles
{
    public class StudentGradesProfiles : Profile
    {
        public StudentGradesProfiles()
        {
            CreateMap<Models.Domain.StudentGrades, Models.DTOs.StudentGrades.GetStudentGrades>();
            CreateMap<Models.Domain.StudentGrades, Models.DTOs.StudentGrades.PostStudentGrades>().ReverseMap();
            CreateMap<Models.Domain.StudentGrades, Models.DTOs.StudentGrades.PatchStudentGrades>().ReverseMap();
        }
    }
}
