using BLL.DTO.BookingDTOs;

namespace BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _unitOfWork.GetRepository<Booking>().GetAllAsync();
            return _mapper.Map<List<BookingDTO>>(bookings);
        }

        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(id);
            return booking != null ? _mapper.Map<BookingDTO>(booking) : null;
        }

        public async Task AddBookingAsync(BookingCreateDTO bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GetRepository<Booking>().InsertAsync(booking);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task UpdateBookingAsync(int id, BookingUpdateDTO bookingDto)
        {
            var repo = _unitOfWork.GetRepository<Booking>();
            var booking = await repo.GetByIdAsync(id);
            if (booking == null) throw new Exception("Booking not found");

            _mapper.Map(bookingDto, booking);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(booking);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public async Task DeleteBookingAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Booking>();
            var booking = await repo.GetByIdAsync(id);
            if (booking == null) throw new Exception("Booking not found");

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
