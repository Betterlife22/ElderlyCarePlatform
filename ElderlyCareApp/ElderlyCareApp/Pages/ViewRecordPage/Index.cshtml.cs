using BLL.DTO.HealthRecordDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages.ViewRecordPage
{
    public class IndexModel : PageModel
    {
        private readonly IHealthRecordService _healthRecordService;

        public IndexModel(IHealthRecordService healthRecordService)
        {
            _healthRecordService = healthRecordService;
        }

        public IList<HealthRecordDTO> HealthRecord { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Index");
            }

            HealthRecord = await _healthRecordService.GetRecordsByUserIdAsync(userId.Value);

            return Page();
        }

    }
}
