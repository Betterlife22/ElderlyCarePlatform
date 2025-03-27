using BLL.DTO.RatingDTOs;

namespace BLL.Interfaces
{
    public interface IRatingService
    {
        Task<List<RatingDTO>> GetAllRatingsAsync();
        Task<RatingDTO?> GetRatingByIdAsync(int id);
        Task AddRatingAsync(RatingCreateDTO ratingDto);
        Task UpdateRatingAsync(int id, RatingUpdateDTO ratingDto);
        Task DeleteRatingAsync(int id);
    }
}
