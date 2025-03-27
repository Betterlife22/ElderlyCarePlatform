using BLL.DTO.CaregiverDTOs;

namespace BLL.Services
{
    public class CaregiverService : ICaregiverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CaregiverService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CaregiverDTO>> GetAllCaregiversAsync()
        {
            var caregivers = await _unitOfWork.GetRepository<Caregiver>().GetAllAsync();
            return _mapper.Map<List<CaregiverDTO>>(caregivers);
        }

        public async Task<CaregiverDTO?> GetCaregiverByIdAsync(int id)
        {
            var caregiver = await _unitOfWork.GetRepository<Caregiver>().GetByIdAsync(id);
            return caregiver != null ? _mapper.Map<CaregiverDTO>(caregiver) : null;
        }

        public async Task AddCaregiverAsync(CaregiverCreateDTO caregiverDto)
        {
            var caregiver = _mapper.Map<Caregiver>(caregiverDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<Caregiver>().InsertAsync(caregiver);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateCaregiverAsync(int id, CaregiverUpdateDTO caregiverDto)
        {
            var repo = _unitOfWork.GetRepository<Caregiver>();
            var caregiver = await repo.GetByIdAsync(id);
            if (caregiver == null) throw new Exception("Caregiver not found");

            _mapper.Map(caregiverDto, caregiver);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(caregiver);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteCaregiverAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Caregiver>();
            var caregiver = await repo.GetByIdAsync(id);
            if (caregiver == null) throw new Exception("Caregiver not found");

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
