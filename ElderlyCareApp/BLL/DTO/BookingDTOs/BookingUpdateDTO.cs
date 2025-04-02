namespace BLL.DTO.BookingDTOs
{
    public class BookingUpdateDTO
    {
        public int Id { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int CaregiverId { get; set; }
        public string Status { get; set; } = null!;
        public string AdminNote { get; set; } = null!;
    }
}
