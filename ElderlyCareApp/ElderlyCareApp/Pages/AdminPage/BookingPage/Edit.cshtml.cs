using BLL.DTO.BookingDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElderlyCareApp.Pages.AdminPage.BookingPage
{
    public class EditModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly ICaregiverService _caregiverService;

        public EditModel(IBookingService bookingService, ICaregiverService caregiverService)
        {
            _bookingService = bookingService;
            _caregiverService = caregiverService;
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

            ViewData["CaregiverId"] = new SelectList(await _caregiverService.GetAllCaregiversAsync(), "Id", "UserName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _bookingService.UpdateBookingAsync(Booking);

            if (result != "Booking updated successfully.")
            {
                ModelState.AddModelError("", result); 
                ViewData["CaregiverId"] = new SelectList(await _caregiverService.GetAllCaregiversAsync(), "Id", "UserName");
                return Page(); // Re-render the page with the validation message
            }

            return RedirectToPage("./Index");
        }
    }
}
