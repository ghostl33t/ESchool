using AutoMapper;

namespace server.Profiles
{
    public class StudentDetailsProfiles : Profile
    {
        public StudentDetailsProfiles()
        {
            CreateMap<Models.Domain.StudentDetails, Models.DTOs.StudentDetails.PostStudentDetails>().ReverseMap();
        }
    }
}
