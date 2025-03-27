namespace BLL.DTO.BookingDTOs
{
    public class BookingUpdateDTO
    {
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = default!;
    }
}
