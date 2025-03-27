using BLL.DTO.CaregiverDTOs;

namespace BLL.Interfaces
{
    public interface ICaregiverService
    {
        Task<List<CaregiverDTO>> GetAllCaregiversAsync();
        Task<CaregiverDTO?> GetCaregiverByIdAsync(int id);
        Task AddCaregiverAsync(CaregiverCreateDTO caregiverDto);
        Task UpdateCaregiverAsync(int id, CaregiverUpdateDTO caregiverDto);
        Task DeleteCaregiverAsync(int id);
    }
}
