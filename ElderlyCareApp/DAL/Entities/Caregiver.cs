namespace DAL.Entities
{
    public class Caregiver : BaseEntity
    {
        public int UserId { get; set; }
        [MaxLength(500)]
        public required string Experience { get; set; }
        [MaxLength(500)]
        public required string Certifications { get; set; }
        [MaxLength(500)]
        public required string Specialization { get; set; }
        public float Rating { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
    }
}
