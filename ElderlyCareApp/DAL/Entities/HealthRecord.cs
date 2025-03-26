namespace DAL.Entities
{
    public class HealthRecord : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime RecoredDate { get; set; }
        public required string BloodPresure { get; set; }
        public int HeartRate { get; set; }
        public float Temperature { get; set; }
        [MaxLength(500)]
        public required string Note { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
