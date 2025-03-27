namespace BLL.DTO.HealthRecordDTOs
{
    public class HealthRecordDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime RecordDate { get; set; }
        public string BloodPressure { get; set; } = default!;
        public int HeartRate { get; set; }
        public float Temperature { get; set; }
        public string Note { get; set; } = default!;
    }
}
