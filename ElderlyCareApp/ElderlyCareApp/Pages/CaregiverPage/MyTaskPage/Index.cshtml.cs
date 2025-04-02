using BLL.DTO.BookingDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages.CaregiverPage.MyTaskPage
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        public string? ErrorMessage { get; set; }

        public IndexModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IList<BookingDTO> Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Index"); 
            }

            Booking = await _bookingService.GetCaregiverBookingAsync(userId.Value);

            return Page();
        }


        // Handle "Mark as Done" button click
        public async Task<IActionResult> OnPostMarkAsDoneAsync(int id)
        {
            var result = await _bookingService.UpdateBookingStatusByScheduleDateAsync(id);

            if (result != "Booking status updated successfully.")
            {
                ModelState.AddModelError(string.Empty, result);
                ErrorMessage = result;
                await OnGetAsync(); 
                return Page(); 
            }

            return RedirectToPage();
        }
    }
}
