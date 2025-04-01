namespace DAL.Entities
{
    public class Booking : BaseEntity
    {
        public int UserId { get; set; }
        public int CaregiverId { get; set; }    
        public int ServiceId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = null!;
        public string? AdminNote { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Caregiver Caregiver { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Receipt>? Receipts { get; set; } = new List<Receipt>();
    }
}
