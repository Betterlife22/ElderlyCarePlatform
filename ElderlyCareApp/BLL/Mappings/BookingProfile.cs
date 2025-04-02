using BLL.DTO.BookingDTOs;
using DAL.Common;

namespace BLL.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.CaregiverName, opt => opt.MapFrom(src => src.Caregiver.User.UserName))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));


            CreateMap<BookingCreateDTO, Booking>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Constants.InReview))
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<BookingUpdateDTO, Booking>()
                .ForMember(dest => dest.CaregiverId, opt => opt.MapFrom(src => src.CaregiverId))
                .ForMember(dest => dest.AdminNote, opt => opt.MapFrom(src => src.AdminNote))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }

}
