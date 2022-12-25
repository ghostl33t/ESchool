using AutoMapper;
namespace server.Profiles
{
    public class ClassDepartmentProfiles : Profile
    {
        public ClassDepartmentProfiles()
        {
            CreateMap<Models.Domain.ClassDepartment, Models.DTOs.ClassDepartment.GetClassDepartment>();
            CreateMap<Models.Domain.ClassDepartment, Models.DTOs.ClassDepartment.PostClassDepartment>().ReverseMap();
            CreateMap<Models.Domain.ClassDepartment, Models.DTOs.ClassDepartment.PatchClassDepartment>().ReverseMap();
        }
    }
}
