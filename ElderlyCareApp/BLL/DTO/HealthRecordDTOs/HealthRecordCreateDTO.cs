namespace BLL.DTO.HealthRecordDTOs
{
    public class HealthRecordCreateDTO
    {
        public int UserId { get; set; }
        public DateTime RecordDate { get; set; }
        public string BloodPresure { get; set; } = default!;
        public int HeartRate { get; set; }
        public float Temperature { get; set; }
        public string Note { get; set; } = default!;
    }
}
