using BLL.DTO.ProfileDTOs;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProfileDTO>> GetAllProfilesAsync()
        {
            var profiles = await _unitOfWork.GetRepository<ElderlyProfile>().GetAllAsync();
            return _mapper.Map<List<ProfileDTO>>(profiles);
        }

        public async Task<ProfileDTO?> GetProfileByIdAsync(int id)
        {
            var profile = await _unitOfWork.GetRepository<ElderlyProfile>().GetByIdAsync(id);
            return profile != null ? _mapper.Map<ProfileDTO>(profile) : null;
        }

        public async Task AddProfileAsync(ProfileCreateDTO profileDto)
        {
            var profile = _mapper.Map<ElderlyProfile>(profileDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<ElderlyProfile>().InsertAsync(profile);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateProfileAsync(int id, ProfileUpdateDTO profileDto)
        {
            var repo = _unitOfWork.GetRepository<ElderlyProfile>();
            var profile = await repo.GetByIdAsync(id);
            if (profile == null) throw new Exception("Profile not found");

            _mapper.Map(profileDto, profile);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(profile);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteProfileAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<ElderlyProfile>();
            var profile = await repo.GetByIdAsync(id);
            if (profile == null) throw new Exception("Profile not found");

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
