using BLL.DTO.UserDTOs;

namespace BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
