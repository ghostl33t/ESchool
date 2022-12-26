using AutoMapper;

namespace server.Profiles
{
    public class SchoolListProfiles : Profile
    {
        public SchoolListProfiles()
        {
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.GetSchoolList>();
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.PostSchoolList>().ReverseMap();
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.PatchUpdate>();
        }
    }
}
