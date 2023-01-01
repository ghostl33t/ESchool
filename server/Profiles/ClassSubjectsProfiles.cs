using AutoMapper;

namespace server.Profiles
{
    public class ClassSubjectsProfiles : Profile
    {
        public ClassSubjectsProfiles()
        {
            CreateMap<Models.Domain.ClassSubjects, Models.DTOs.ClassSubjects.GetClassSubject>();
            CreateMap<Models.Domain.ClassSubjects, Models.DTOs.ClassSubjects.PostClassSubjects>().ReverseMap();
            CreateMap<Models.Domain.ClassSubjects, Models.DTOs.ClassSubjects.PatchClassSubjects>().ReverseMap();
        }
    }
}
