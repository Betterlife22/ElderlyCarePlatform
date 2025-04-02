using BLL.DTO.CaregiverDTOs;
using BLL.DTO.ServiceDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IService _service;
        private readonly ICaregiverService _caregiverService;
        [BindProperty]
        public List<ServiceDTO> Services { get; set; }
        [BindProperty]
        public List<CaregiverDTO> Caregivers { get; set; }
        public IndexModel(ILogger<IndexModel> logger,  IService service, ICaregiverService caregiverService)
        {
            _service = service;
            _caregiverService = caregiverService;
            _logger = logger;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            Services = await _service.GetAllServicesAsync();
            Caregivers = await _caregiverService.GetAllCaregiversAsync();
            return Page();
        }
    }
}
