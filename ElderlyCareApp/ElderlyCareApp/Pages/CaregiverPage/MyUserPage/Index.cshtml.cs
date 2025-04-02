using BLL.DTO.HealthRecordDTOs;
using BLL.DTO.UserDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages.CaregiverPage.MyUserPage
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IHealthRecordService _healthRecordService;
        public string SuccessMessage { get; set; }

        public IndexModel(IBookingService bookingService, IHealthRecordService healthRecordService)
        {
            _bookingService = bookingService;
            _healthRecordService = healthRecordService;
        }

        public IList<UserDTO> User { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Index");
            }

            User = await _bookingService.GetCaregiverCustomersAsync(userId.Value);

            return Page();
        }


        public async Task<IActionResult> OnPostAddHealthRecordAsync(HealthRecordCreateDTO healthRecord)
        {
            await _healthRecordService.AddRecordAsync(healthRecord);

            SuccessMessage = "Send Report to the customer successfully.";
            await OnGetAsync();
            return Page();
        }
    }
}
