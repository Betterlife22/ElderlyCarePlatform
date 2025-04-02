using BLL.DTO.HealthRecordDTOs;

namespace BLL.Interfaces
{
    public interface IHealthRecordService
    {
        Task<List<HealthRecordDTO>> GetAllRecordsAsync();
        Task<HealthRecordDTO?> GetRecordByIdAsync(int id);
        Task AddRecordAsync(HealthRecordCreateDTO recordDto);
        Task UpdateRecordAsync(int id, HealthRecordUpdateDTO recordDto);
        Task DeleteRecordAsync(int id);
        Task<List<HealthRecordDTO>> GetRecordsByUserIdAsync(int userId);
    }
}
