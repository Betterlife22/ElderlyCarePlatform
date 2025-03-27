using BLL.DTO.BookingDTOs;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task AddBookingAsync(BookingCreateDTO bookingDto);
        Task UpdateBookingAsync(int id, BookingUpdateDTO bookingDto);
        Task DeleteBookingAsync(int id);
    }
}
