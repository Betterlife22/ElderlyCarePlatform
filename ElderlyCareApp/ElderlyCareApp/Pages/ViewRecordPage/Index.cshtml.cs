using BLL.DTO.HealthRecordDTOs;
using BLL.Interfaces;
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

        public async Task OnGetAsync()
        {
            int userId = 9; //Hardcode
            HealthRecord = await _healthRecordService.GetRecordsByUserIdAsync(userId);
        }
    }
}
