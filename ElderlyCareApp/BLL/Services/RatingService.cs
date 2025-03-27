using BLL.DTO.RatingDTOs;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RatingDTO>> GetAllRatingsAsync()
        {
            var ratings = await _unitOfWork.GetRepository<Rating>().GetAllAsync();
            return _mapper.Map<List<RatingDTO>>(ratings);
        }

        public async Task<RatingDTO?> GetRatingByIdAsync(int id)
        {
            var rating = await _unitOfWork.GetRepository<Rating>().GetByIdAsync(id);
            return rating != null ? _mapper.Map<RatingDTO>(rating) : null;
        }

        public async Task AddRatingAsync(RatingCreateDTO ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<Rating>().InsertAsync(rating);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateRatingAsync(int id, RatingUpdateDTO ratingDto)
        {
            var repo = _unitOfWork.GetRepository<Rating>();
            var rating = await repo.GetByIdAsync(id);
            if (rating == null) throw new Exception("Rating not found");

            _mapper.Map(ratingDto, rating);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(rating);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteRatingAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Rating>();
            var rating = await repo.GetByIdAsync(id);
            if (rating == null) throw new Exception("Rating not found");

            _unitOfWork.BeginTransaction();
            try
            {
                await repo.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
