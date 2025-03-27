using BLL.DTO.UserDTOs;

namespace BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.GuardianName, opt => opt.MapFrom(src => src.ElderlyProfile != null ? src.ElderlyProfile.GuardianName : null))
                .ForMember(dest => dest.GuardianAge, opt => opt.MapFrom(src => src.ElderlyProfile != null ? src.ElderlyProfile.GuardianAge : (int?)null))
                .ForMember(dest => dest.GuardianIdCard, opt => opt.MapFrom(src => src.ElderlyProfile != null ? src.ElderlyProfile.GuardianIdCard : null));

            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.ElderlyProfile, opt => opt.MapFrom(src =>
                    src.GuardianName != null ? new ElderlyProfile
                    {
                        GuardianName = src.GuardianName,
                        GuardianAge = src.GuardianAge ?? 0,
                        GuardianIdCard = src.GuardianIdCard ?? string.Empty
                    } : null
                ));

            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.ElderlyProfile, opt => opt.MapFrom((src, dest) =>
                {
                    if (src.GuardianName != null)
                    {
                        dest.ElderlyProfile ??= new ElderlyProfile();
                        dest.ElderlyProfile.GuardianName = src.GuardianName;
                        dest.ElderlyProfile.GuardianAge = src.GuardianAge ?? 0;
                        dest.ElderlyProfile.GuardianIdCard = src.GuardianIdCard ?? string.Empty;
                    }
                    else
                    {
                        dest.ElderlyProfile = null; // Remove guardian info if not provided
                    }
                    return dest.ElderlyProfile;
                }));
        }
    }
}
