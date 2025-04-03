using BLL.DTO.RatingDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages.AdminPage.RatingPage
{
    public class IndexModel : PageModel
    {
        private readonly IRatingService _ratingService;

        public IndexModel(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        public IList<RatingDTO> Rating { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/Auth/Index");
            }

            Rating = await _ratingService.GetAllRatingsAsync();

            return Page();
        }
    }
}
