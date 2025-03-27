namespace BLL.DTO.HealthRecordDTOs
{
    public class HealthRecordUpdateDTO
    {
        public DateTime RecordDate { get; set; }
        public string BloodPressure { get; set; } = default!;
        public int HeartRate { get; set; }
        public float Temperature { get; set; }
        public string Note { get; set; } = default!;
    }
}
