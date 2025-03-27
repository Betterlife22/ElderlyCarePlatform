using DAL.Common;

namespace BLL.DTO.ReceiptDTOs
{
    public class ReceiptUpdateDTO
    {
        public float Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
    }
}
