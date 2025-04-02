using BLL.DTO.RatingDTOs;
using BLL.Interfaces;
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

        public async Task OnGetAsync()
        {
            Rating = await _ratingService.GetAllRatingsAsync();
        }
    }
}
