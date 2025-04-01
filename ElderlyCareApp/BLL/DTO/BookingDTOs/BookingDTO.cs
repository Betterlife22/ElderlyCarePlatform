namespace BLL.DTO.BookingDTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int CaregiverId { get; set; }
        public string UserName { get; set; } = null!;
        public string CaregiverName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = default!;
    }
}
