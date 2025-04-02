namespace BLL.DTO.BookingDTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int CaregiverId { get; set; }
        public string CaregiverName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public float Total { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = default!;
    }
}
