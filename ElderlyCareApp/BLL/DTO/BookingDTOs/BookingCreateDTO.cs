using DAL.Common;

namespace BLL.DTO.BookingDTOs
{
    public class BookingCreateDTO
    {
        public int UserId { get; set; }
        public int CaregiverId { get; set; }
        public int ServiceId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = Constants.InProccess;
    }
}
