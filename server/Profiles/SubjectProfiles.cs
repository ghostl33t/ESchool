using AutoMapper;
namespace server.Profiles
{
    public class SubjectProfiles : Profile
    {
        public SubjectProfiles()
        {
            CreateMap<Models.Domain.Subject, Models.DTOs.Subject.GetSubject>();
            CreateMap<Models.Domain.Subject, Models.DTOs.Subject.PostSubject>().ReverseMap();
            CreateMap<Models.Domain.Subject, Models.DTOs.Subject.PatchSubject>().ReverseMap();
        }
    }
}
