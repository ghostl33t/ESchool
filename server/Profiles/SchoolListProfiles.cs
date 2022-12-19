using AutoMapper;

namespace server.Profiles
{
    public class SchoolListProfiles : Profile
    {
        public SchoolListProfiles()
        {
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.SchoolList>();
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.Create>().ReverseMap();
            CreateMap<Models.Domain.SchoolList, Models.DTOs.SchoolList.Update>();
        }
    }
}
