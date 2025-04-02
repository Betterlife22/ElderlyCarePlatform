using ChatFPT.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ElderlyCareApp.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly IAIService _aiService;

        [BindProperty]
        public string Message { get; set; } = string.Empty;

        public IndexModel(IAIService aiService)
        {
            _aiService = aiService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostQueryAsync()
        {
            if (string.IsNullOrEmpty(Message))
            {
                return BadRequest("Message cannot be empty");
            }

            //int userId = HttpContext.Session.GetInt32("UserID")!.Value;

            var response = await _aiService.QueryDataAsync(Message, -1);
            return Content(response);
        }
    }
}