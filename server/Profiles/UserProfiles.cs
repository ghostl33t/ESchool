using AutoMapper;

namespace server.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<Models.Domain.User, Models.DTOs.UsersDTO.UsersDTO>();
            CreateMap<Models.Domain.User, Models.DTOs.UsersDTO.Create>().ReverseMap();
            CreateMap<Models.Domain.User, Models.DTOs.UsersDTO.Update>();
        }
    }
}
