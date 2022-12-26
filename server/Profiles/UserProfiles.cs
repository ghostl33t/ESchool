using AutoMapper;

namespace server.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<Models.Domain.User, Models.DTOs.UsersDTO.GetUser>();
            CreateMap<Models.Domain.User, Models.DTOs.UsersDTO.PostUser>().ReverseMap();
            CreateMap<Models.Domain.User, Models.DTOs.UsersDTO.PatchUser>().ReverseMap();
        }
    }
}
