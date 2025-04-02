using BLL.DTO.BookingDTOs;
using BLL.DTO.UserDTOs;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();
        Task<List<BookingDTO>> GetCaregiverBookingAsync(int caregiverId);
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task AddBookingAsync(BookingCreateDTO model);
        Task UpdateStatusBooking(int id);
        Task<string> UpdateBookingAsync(BookingUpdateDTO bookingDto);
        Task DeleteBookingAsync(int id);
        Task<string> UpdateBookingStatusByScheduleDateAsync(int bookingId);
        Task<List<UserDTO>> GetCaregiverCustomersAsync(int caregiverId);
    }
}
