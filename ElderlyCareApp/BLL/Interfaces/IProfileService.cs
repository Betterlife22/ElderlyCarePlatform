using BLL.DTO.ProfileDTOs;

namespace BLL.Interfaces
{
    public interface IProfileService
    {
        Task<List<ProfileDTO>> GetAllProfilesAsync();
        Task<ProfileDTO?> GetProfileByIdAsync(int id);
        Task AddProfileAsync(ProfileCreateDTO profileDto);
        Task UpdateProfileAsync(int id, ProfileUpdateDTO profileDto);
        Task DeleteProfileAsync(int id);
    }
}
