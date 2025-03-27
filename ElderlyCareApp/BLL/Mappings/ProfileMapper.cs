using BLL.DTO.ProfileDTOs;

namespace BLL.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<ElderlyProfile, ProfileDTO>();

            CreateMap<ProfileCreateDTO, ElderlyProfile>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<ProfileUpdateDTO, ElderlyProfile>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
