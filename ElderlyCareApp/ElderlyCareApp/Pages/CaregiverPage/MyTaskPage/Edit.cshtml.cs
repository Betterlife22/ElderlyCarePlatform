using BLL.DTO.BookingDTOs;
using BLL.Interfaces;
using ElderlyCareApp.SignalRHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ElderlyCareApp.Pages.CaregiverPage.MyTaskPage
{
    public class EditModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IHubContext<HubSignalR> _hubContext;

        public EditModel(IBookingService bookingService, IHubContext<HubSignalR> hubContext)
        {
            _hubContext = hubContext;   
            _bookingService = bookingService;
        }

        [BindProperty]
        public BookingUpdateDTO Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDTO = await _bookingService.GetBookingByIdAsync(id.Value);
            if (bookingDTO == null)
            {
                return NotFound();
            }

            Booking = new BookingUpdateDTO
            {
                Id = bookingDTO.Id,
                ScheduleDate = bookingDTO.ScheduleDate,
                CaregiverId = bookingDTO.CaregiverId,
                Status = bookingDTO.Status,
                AdminNote = ""
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _bookingService.UpdateBookingAsync(Booking);

            if (result != "Booking updated successfully.")
            {

                ModelState.AddModelError("", result);
                return Page(); // Re-render the page with the validation message
            }
            await _hubContext.Clients.All.SendAsync("UpdateStatus", Booking.Id);

            return RedirectToPage("./Index");
        }
    }
}
