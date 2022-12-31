using AutoMapper;
using server.Models.Domain;
using server.Models.DTOs.ProfessorSubjects;

namespace server.Profiles;

public class ProfessorSubjectsProfile : Profile
{
    public ProfessorSubjectsProfile()
    {
        CreateMap<ProfessorSubjects, GetProfessorSubjects>();
        CreateMap<ProfessorSubjects, PostProfessorSubjects>().ReverseMap();
        CreateMap<ProfessorSubjects, PatchProfessorSubjects>().ReverseMap();
    }
}
