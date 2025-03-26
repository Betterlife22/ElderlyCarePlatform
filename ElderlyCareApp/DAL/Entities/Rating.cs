namespace DAL.Entities
{
    public class Rating : BaseEntity
    {
        public int BookingId { get; set; }
        public int CaregiverId { get; set; }
        public int UserId { get; set; }
        public int RatingScore { get; set; }
        [MaxLength(500)]
        public string Review { get; set; } = null!;

        public virtual Booking Booking { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Caregiver Caregiver { get; set; } = null!;
    }
}
 