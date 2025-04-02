using DAL.Common;

namespace BLL.DTO.BookingDTOs
{
    public class BookingCreateDTO
    {
        public int UserId { get; set; }
        public int CaregiverId { get; set; }
        public int ServiceId { get; set; }
        [FutureDateTime(ErrorMessage = "Time or Date is not valid")]
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = Constants.Processing;
    }
}
