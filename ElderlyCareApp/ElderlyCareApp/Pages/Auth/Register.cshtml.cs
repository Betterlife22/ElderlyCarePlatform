using BLL.DTO.UserDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElderlyCareApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string Role { get; set; } = string.Empty;

        [BindProperty]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Gender { get; set; } = string.Empty;

        [BindProperty]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        public string BirthDate { get; set; } = string.Empty;

        [BindProperty]
        public string? GuardianName { get; set; }

        [BindProperty]
        public int GuardianAge { get; set; }

        [BindProperty]
        public string? GuardianIdCard { get; set; }

        [BindProperty]
        public bool ShowGuardianFields { get; set; } = false;

        public void OnPost()
        {
            var user = new UserCreateDTO
            {
                Email = Email,
                Password = Password,
                Role = Role,
                UserName = UserName,
                PhoneNumber = PhoneNumber,
                Gender = Gender,
                Address = Address,
                BirthDate = DateTime.Parse(BirthDate)
            };

            if (GuardianName != null)
            {
                user.GuardianName = GuardianName;
                user.GuardianAge = GuardianAge;
                user.GuardianIdCard = GuardianIdCard;
            }

            _userService.RegisterAsync(user);
        }

        public IActionResult OnPostToggleGuardian()
        {
            ShowGuardianFields = !ShowGuardianFields;
            return Page();
        }
    }
}
