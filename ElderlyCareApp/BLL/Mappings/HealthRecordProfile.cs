using BLL.DTO.HealthRecordDTOs;

namespace BLL.Mappings
{
    public class HealthRecordProfile : Profile
    {
        public HealthRecordProfile()
        {
            CreateMap<HealthRecord, HealthRecordDTO>();

            CreateMap<HealthRecordCreateDTO, HealthRecord>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<HealthRecordUpdateDTO, HealthRecord>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
