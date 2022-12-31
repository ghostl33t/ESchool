using AutoMapper;
using server.Models.Domain;
using server.Models.DTOs.StudentDetails;

namespace server.Profiles
{
    public class StudentDetailsProfiles : Profile
    {
        public StudentDetailsProfiles()
        {
            CreateMap<StudentDetails, PostStudentDetails>().ReverseMap();
            CreateMap<StudentDetails, PatchStudentDetails>().ReverseMap();
        }
    }
}
