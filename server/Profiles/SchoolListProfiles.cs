using AutoMapper;

namespace server.Profiles
{
    public class SchoolListProfiles : Profile
    {
        public SchoolListProfiles()
        {
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>();
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.Create>();
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.Update>();
        }
    }
}
