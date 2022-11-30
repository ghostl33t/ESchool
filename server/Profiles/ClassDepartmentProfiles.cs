using AutoMapper;
namespace server.Profiles
{
    public class ClassDepartmentProfiles : Profile
    {
        public ClassDepartmentProfiles()
        {
            CreateMap<Models.Domain.ClassDepartment, Models.DTOs.ClassDepartment.ClassDepartmentDTO>();
            CreateMap<Models.Domain.ClassDepartment, Models.DTOs.ClassDepartment.Create>().ReverseMap();
            CreateMap<Models.Domain.ClassDepartment, Models.DTOs.ClassDepartment.Update>();
        }
    }
}
