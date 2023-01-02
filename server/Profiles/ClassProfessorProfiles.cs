using AutoMapper;

namespace server.Profiles
{
    public class ClassProfessorProfiles : Profile
    {
        public ClassProfessorProfiles()
        {
            CreateMap<Models.Domain.ClassProfessors, Models.DTOs.ClassProfessors.GetClassProfessors>();
            CreateMap<Models.Domain.ClassProfessors, Models.DTOs.ClassProfessors.PostClassProfessors>().ReverseMap();
            CreateMap<Models.Domain.ClassProfessors, Models.DTOs.ClassProfessors.PatchClassProfessors>().ReverseMap();
        }
    }
}
