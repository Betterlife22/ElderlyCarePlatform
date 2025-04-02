namespace BLL.DTO.HealthRecordDTOs
{
    public class HealthRecordDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = default!;
        public DateTime RecordDate { get; set; }
        public string BloodPressure { get; set; } = default!;
        public int HeartRate { get; set; }
        public float Temperature { get; set; }
        public string Note { get; set; } = default!;
    }
}
