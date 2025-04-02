using BLL.DTO.BookingDTOs;
using DAL.Common;
using Microsoft.EntityFrameworkCore;

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
            var bookings = await _unitOfWork.GetRepository<Booking>().GetAllAsync(includeProperties: "User,Caregiver.User,Service");
            return _mapper.Map<List<BookingDTO>>(bookings);
        }

        public async Task<List<BookingDTO>> GetCaregiverBookingAsync(int caregiverUserId)
        {
            var bookings = await _unitOfWork.GetRepository<Booking>()
                .GetAllAsync(
                    filter: b => b.Caregiver.UserId == caregiverUserId,
                    includeProperties: "User,Caregiver.User,Service"
                );

            return _mapper.Map<List<BookingDTO>>(bookings);
        }

        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().GetByPropertyAsync(b => b.Id == id, includeProperties: "User,Caregiver.User,Service");
            return booking != null ? _mapper.Map<BookingDTO>(booking) : null;
        }


        public async Task AddBookingAsync(BookingCreateDTO model)
        {          
            Booking booking = _mapper.Map<Booking>(model);
            booking.Status = Constants.Processing;
            booking.Created = DateTime.Now;
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

        public async Task UpdateStatusBooking(int id)
        {
            Booking booking = await _unitOfWork.GetRepository<Booking>().FindAsync(id);
            booking.Status = Constants.Cancelled;
                await _unitOfWork.GetRepository<Booking>().UpdateAsync(booking);
                await _unitOfWork.SaveAsync();
        }

        public async Task<string> UpdateBookingAsync(BookingUpdateDTO bookingDto)
        {
            var repo = _unitOfWork.GetRepository<Booking>();
            var booking = await repo.GetByIdAsync(bookingDto.Id);

            if (booking == null)
                return "Booking not found";


            // Check if the new schedule date is in the past
            if (bookingDto.ScheduleDate < DateTime.Today)
                return "Cannot update booking to a past date.";

            // If status is "Cancelled", skip other checks
            if (bookingDto.Status == "Cancelled")
            {
                _mapper.Map(bookingDto, booking);

                _unitOfWork.BeginTransaction();
                try
                {
                    repo.Update(booking);
                    await _unitOfWork.SaveAsync();
                    _unitOfWork.CommitTransaction();
                    return "Booking cancelled successfully.";
                }
                catch
                {
                    _unitOfWork.RollBack();
                    return "An error occurred while cancelling the booking.";
                }
            }

            // If status is being updated to "Confirmed", check caregiver availability
            if (bookingDto.Status == Constants.Confirmed)
            {
                if (await IsCaregiverBusyAsync(bookingDto.CaregiverId, bookingDto.ScheduleDate, bookingDto.Id))
                    return "You already have an appointment on this day.";
            }

            // Check if caregiver is available on the new schedule date
            if (await IsCaregiverBusyAsync(bookingDto.CaregiverId, bookingDto.ScheduleDate, bookingDto.Id))
                return "You already have an appointment on this day.";


            // Map updated values
            _mapper.Map(bookingDto, booking);

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(booking);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return "Booking updated successfully.";
            }
            catch
            {
                _unitOfWork.RollBack();
                return "An error occurred while updating the booking.";
            }
        }

        private async Task<bool> IsCaregiverBusyAsync(int caregiverId, DateTime scheduleDate, int excludeBookingId = 0)
        {
            return await _unitOfWork.GetRepository<Booking>()
                .Entities
                .AnyAsync(b => b.CaregiverId == caregiverId &&
                               b.ScheduleDate.Date == scheduleDate.Date &&
                               (b.Status == Constants.Confirmed) &&
                               b.Id != excludeBookingId);
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

        public async Task<string> UpdateBookingStatusByScheduleDateAsync(int bookingId)
        {
            var repo = _unitOfWork.GetRepository<Booking>();
            var booking = await repo.GetByIdAsync(bookingId);

            if (booking == null)
                return "Booking not found.";

            DateTime now = DateTime.Now;
            DateTime scheduleDate = booking.ScheduleDate;

            if (now.Date < scheduleDate.Date)
                return "The schedule date is not yet."; // Future booking

            if (now.Date == scheduleDate.Date && now.TimeOfDay >= scheduleDate.TimeOfDay)
            {
                booking.Status = Constants.Completed;
            }
            else if (now.Date >= scheduleDate.Date.AddDays(2)) // Two days after schedule
            {
                booking.Status = Constants.Overdue;
            }
            else
            {
                return "The booking cannot be marked as done yet.";
            }

            _unitOfWork.BeginTransaction();
            try
            {
                repo.Update(booking);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return "Booking status updated successfully.";
            }
            catch
            {
                _unitOfWork.RollBack();
                return "An error occurred while updating the booking status.";
            }
        }
    }
}
