namespace DAL.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = Constants.Male;
        [MaxLength(100)]
        public required string Address { get; set; }
        public virtual ElderlyProfile ElderlyProfile { get; set; } = new();
        public virtual ICollection<HealthRecord>? HealthRecords { get; set; } = new List<HealthRecord>();
        public virtual ICollection<Caregiver>? Caregivers { get; set; } = new List<Caregiver>();
        public virtual ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Receipt>? Receipts { get; set; } = new List<Receipt>();
    }
}