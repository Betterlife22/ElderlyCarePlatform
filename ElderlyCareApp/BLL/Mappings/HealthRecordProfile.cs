using BLL.DTO.HealthRecordDTOs;

namespace BLL.Mappings
{
    public class HealthRecordProfile : Profile
    {
        public HealthRecordProfile()
        {
            CreateMap<HealthRecord, HealthRecordDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.BloodPressure, opt => opt.MapFrom(src => src.BloodPresure));


            CreateMap<HealthRecordCreateDTO, HealthRecord>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.BloodPresure, opt => opt.MapFrom(src => src.BloodPresure))
                .ForMember(dest => dest.RecoredDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.HeartRate, opt => opt.MapFrom(src => src.HeartRate))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));


            CreateMap<HealthRecordUpdateDTO, HealthRecord>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
