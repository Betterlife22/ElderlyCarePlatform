using DAL.Common;

namespace BLL.DTO.ReceiptDTOs
{
    public class ReceiptDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public float Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
    }
}
