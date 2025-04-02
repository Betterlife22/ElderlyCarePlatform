using BLL.DTO.ReceiptDTOs;
using DAL.Libraries;

namespace BLL.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReceiptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ReceiptDTO>> GetAllReceiptsAsync()
        {
            var receipts = await _unitOfWork.GetRepository<Receipt>().GetAllAsync();
            return _mapper.Map<List<ReceiptDTO>>(receipts);
        }

        public async Task<ReceiptDTO?> GetReceiptByIdAsync(int id)
        {
            var receipt = await _unitOfWork.GetRepository<Receipt>().GetByIdAsync(id);
            return receipt != null ? _mapper.Map<ReceiptDTO>(receipt) : null;
        }

        public async Task <Receipt> AddReceiptAsync(ReceiptCreateDTO receiptDto)
        {
            var receipt = _mapper.Map<Receipt>(receiptDto);
            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<Receipt>().InsertAsync(receipt);
                
                
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return receipt;
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
        public async Task UpdateReceiptStatus (int receiptId)
        {
            try
            {
                var receipt = await _unitOfWork.GetRepository<Receipt>().GetByIdAsync(receiptId);
                receipt.Status = "Success";
                var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(receipt.BookingId);
                booking.Status = "Completed";
                await _unitOfWork.SaveAsync(); 
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
            
        }

        public async Task UpdateReceiptAsync(int id, ReceiptUpdateDTO receiptDto)
        {
            var repo = _unitOfWork.GetRepository<Receipt>();
            var receipt = await repo.GetByIdAsync(id);
            if (receipt == null) throw new Exception("Receipt not found");

            _mapper.Map(receiptDto, receipt);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(receipt);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteReceiptAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Receipt>();
            var receipt = await repo.GetByIdAsync(id);
            if (receipt == null) throw new Exception("Receipt not found");

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
