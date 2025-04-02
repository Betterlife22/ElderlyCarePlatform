using BLL.DTO.RatingDTOs;

namespace BLL.Mappings
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingDTO>()
                .ForMember(dest => dest.CaregiverName, opt => opt.MapFrom(src => src.Caregiver.User.UserName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));


            CreateMap<RatingCreateDTO, Rating>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<RatingUpdateDTO, Rating>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
