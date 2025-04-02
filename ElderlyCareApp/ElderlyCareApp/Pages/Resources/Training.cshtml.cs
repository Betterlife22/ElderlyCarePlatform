using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElderlyCareApp.Models;
using ChatFPT.Service.Interfaces; // Assuming you have a Models folder

namespace ElderlyCareApp.Pages.Resources
{
    public class TrainingModel : PageModel
    {

        private readonly IAIService _aiService;

        public TrainingModel(IAIService aiService)
        {
            _aiService = aiService;
        }

        [BindProperty]
        public UploadDataModel UploadDataModel { get; set; } = null!;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _aiService.UploadDataToPineconeAsync(UploadDataModel);

            return Page();
        }
    }
}