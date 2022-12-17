using AutoMapper;
namespace server.Profiles
{
    public class CDSP : Profile
    {
        public CDSP()
        {
            CreateMap<Models.Domain.ClassDepartmentSubjectProfessor, Models.DTOs.ClassDepartmentSubjectProfessor.ClassDepartmentSubjectProfessorDTO>();
            CreateMap<Models.Domain.ClassDepartmentSubjectProfessor, Models.DTOs.ClassDepartmentSubjectProfessor.Create>().ReverseMap();
            CreateMap<Models.Domain.ClassDepartmentSubjectProfessor, Models.DTOs.ClassDepartmentSubjectProfessor.Update>();
        }
    }
}
