using BLL.DTO.HealthRecordDTOs;

namespace BLL.Services
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HealthRecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HealthRecordDTO>> GetAllRecordsAsync()
        {
            var records = await _unitOfWork.GetRepository<HealthRecord>().GetAllAsync();
            return _mapper.Map<List<HealthRecordDTO>>(records);
        }

        public async Task<List<HealthRecordDTO>> GetRecordsByUserIdAsync(int userId)
        {
            var records = await _unitOfWork.GetRepository<HealthRecord>()
                .GetAllAsync(
                    filter: r => r.UserId == userId,
                    includeProperties: "User"
                );

            return _mapper.Map<List<HealthRecordDTO>>(records);
        }

        public async Task<HealthRecordDTO?> GetRecordByIdAsync(int id)
        {
            var record = await _unitOfWork.GetRepository<HealthRecord>().GetByIdAsync(id);
            return record != null ? _mapper.Map<HealthRecordDTO>(record) : null;
        }

        public async Task AddRecordAsync(HealthRecordCreateDTO recordDto)
        {
            var record = _mapper.Map<HealthRecord>(recordDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<HealthRecord>().InsertAsync(record);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateRecordAsync(int id, HealthRecordUpdateDTO recordDto)
        {
            var repo = _unitOfWork.GetRepository<HealthRecord>();
            var record = await repo.GetByIdAsync(id);
            if (record == null) throw new Exception("Health record not found");

            _mapper.Map(recordDto, record);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(record);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteRecordAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<HealthRecord>();
            var record = await repo.GetByIdAsync(id);
            if (record == null) throw new Exception("Health record not found");

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
