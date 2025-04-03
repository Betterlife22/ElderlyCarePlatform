using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Interfaces;
using BLL.DTO.BookingDTOs;
using BLL.DTO.ServiceDTOs;

namespace ElderlyCareApp.Pages.BookingServicePage
{
    public class CreateModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly ICaregiverService _careService;
        private readonly IService _serviceService;
        private readonly IUserService _userService;
        [BindProperty] 
        public ServiceDTO Service {  get; set; }
        public CreateModel(IBookingService bookingService, ICaregiverService careService, IService serviceService, IUserService userService)
        {
            _bookingService = bookingService;
            _careService = careService;
            _serviceService = serviceService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            ViewData["CaregiverId"] = new SelectList(await _careService.GetAllCaregiversAsync(), "Id", "UserName");
            ViewData["ServiceId"] = new SelectList(await _serviceService.GetAllServicesAsync(), "Id", "Name");
            Service = await _serviceService.GetServiceByIdAsync(id);
            return Page();
        }

        [BindProperty]
        public BookingCreateDTO Booking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            int? userId = HttpContext.Session.GetInt32("UserID");

            Booking.UserId = userId!.Value;
            try
            {
                await _bookingService.AddBookingAsync(Booking);
                
                TempData["Message"] = "Create Booking Successfully !!!"; 
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }          
            return RedirectToPage("../Booking/BookingList");
        }
    }
}
