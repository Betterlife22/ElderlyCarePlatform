using DAL.Common;

namespace BLL.DTO.ReceiptDTOs
{
    public class ReceiptCreateDTO
    {
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public float Ammount { get; set; }
        public string? PaymentMethod { get; set; }
        public string Status { get; set; } = Constants.Pending;
    }
}
