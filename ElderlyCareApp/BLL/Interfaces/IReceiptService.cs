using BLL.DTO.ReceiptDTOs;

namespace BLL.Interfaces
{
    public interface IReceiptService
    {
        Task<List<ReceiptDTO>> GetAllReceiptsAsync();
        Task<ReceiptDTO?> GetReceiptByIdAsync(int id);
        Task AddReceiptAsync(ReceiptCreateDTO receiptDto);
        Task UpdateReceiptAsync(int id, ReceiptUpdateDTO receiptDto);
        Task DeleteReceiptAsync(int id);
    }
}
