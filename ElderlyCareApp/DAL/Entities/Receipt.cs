namespace DAL.Entities
{
    public class Receipt : BaseEntity
    {
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public float Ammount { get; set; }
        public string? PaymentMethod { get; set; } 
        public string? Status { get; set; } 
        public virtual User User { get; set; } = null!;
        public virtual Booking Booking{ get; set; } = null!;
    }
}
