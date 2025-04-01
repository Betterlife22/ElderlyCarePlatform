namespace DAL.Entities
{
    public class ElderlyProfile : BaseEntity
    {
        public int UserId { get; set; }
        [MaxLength(500)]
        public string MedicalHistory { get; set; } = default!;
        [MaxLength(200)]
        public string Preferences { get; set; } = default!;
        [MaxLength(100)]
        public string GuardianName { get; set; } = default!;
        public int GuardianAge { get; set; }
        public string GuardianIdCard { get; set; } = default!;
        [MaxLength(300)]
        public string HealthConditions { get; set; } = default!;
        public virtual User? User { get; set; }
    }
}
