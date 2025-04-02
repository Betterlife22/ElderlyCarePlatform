﻿using BLL.DTO.BookingDTOs;

namespace BLL.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task AddBookingAsync(BookingCreateDTO bookingDto);
        Task<string> UpdateBookingAsync(BookingUpdateDTO bookingDto);
        Task DeleteBookingAsync(int id);
    }
}
