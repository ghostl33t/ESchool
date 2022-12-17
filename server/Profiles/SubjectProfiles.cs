using AutoMapper;
namespace server.Profiles
{
    public class SubjectProfiles : Profile
    {
        public SubjectProfiles()
        {
            CreateMap<Models.Domain.Subject, Models.DTOs.Subject.SubjectDTO>();
            CreateMap<Models.Domain.Subject, Models.DTOs.Subject.Create>().ReverseMap();
            CreateMap<Models.Domain.Subject, Models.DTOs.Subject.Update>();
        }
    }
}
