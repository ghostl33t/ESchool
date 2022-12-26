using AutoMapper;
namespace server.Profiles
{
    public class CDSP : Profile
    {
        public CDSP()
        {
            //CreateMap<Models.Domain.ClassDepartmentSubjectProfessor, Models.DTOs.ClassDepartmentSubjectProfessor.PostCDSP>();
            CreateMap<Models.Domain.ClassDepartmentSubjectProfessor, Models.DTOs.ClassDepartmentSubjectProfessor.PostCDSP>().ReverseMap();
            CreateMap<Models.Domain.ClassDepartmentSubjectProfessor, Models.DTOs.ClassDepartmentSubjectProfessor.PatchCDSP>().ReverseMap();
        }
    }
}
