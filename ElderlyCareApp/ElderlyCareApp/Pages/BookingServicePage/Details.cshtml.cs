using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTO.BookingDTOs;
using DAL.Common;

namespace ElderlyCareApp.Pages.BookingServicePage
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public DetailsModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public BookingDTO Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var booking = await _bookingService.GetBookingByIdAsync(id);

            Booking = booking;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
                       
            await _bookingService.UpdateStatusBooking(id);

            return RedirectToPage("./Index");
        }
    }
}
