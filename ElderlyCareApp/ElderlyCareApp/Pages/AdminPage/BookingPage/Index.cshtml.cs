using BLL.DTO.BookingDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages.AdminPage.BookingPage
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
