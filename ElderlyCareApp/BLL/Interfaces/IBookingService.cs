using BLL.DTO.BookingDTOs;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking model);
        Task UpdateStatusBooking(int id);
        Task<string> UpdateBookingAsync(BookingUpdateDTO bookingDto);
        Task DeleteBookingAsync(int id);
    }
}
