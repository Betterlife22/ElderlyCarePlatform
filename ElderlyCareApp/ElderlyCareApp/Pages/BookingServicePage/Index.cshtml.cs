using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTO.BookingDTOs;

namespace ElderlyCareApp.Pages.BookingServicePage
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public IndexModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IList<BookingDTO> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Booking = await _bookingService.GetAllBookingsAsync();
        }
    }
}
