using BLL.DTO.BookingDTOs;
using DAL.Common;

namespace BLL.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>();

            CreateMap<BookingCreateDTO, Booking>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Constants.InReview))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<BookingUpdateDTO, Booking>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }

}
