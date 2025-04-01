using BLL.DTO.CaregiverDTOs;

namespace BLL.Mappings
{
    public class CaregiverProfile : Profile
    {
        public CaregiverProfile()
        {
            CreateMap<Caregiver, CaregiverDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<CaregiverCreateDTO, Caregiver>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                //.ForMember(dest => dest.Rating, opt => opt.MapFrom(_ => 0)); //No data yet
            CreateMap<CaregiverUpdateDTO, Caregiver>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }

}
