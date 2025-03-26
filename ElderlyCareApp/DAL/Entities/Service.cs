namespace DAL.Entities
{
    public class Service : BaseEntity
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(100)]
        public required string Description { get; set; }
        public required float Price { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    }
}
