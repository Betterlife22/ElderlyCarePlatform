using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BLL.Services;
using BLL.Interfaces;

public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    public LoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string LoginType { get; set; } = "User";

    public async Task<IActionResult> OnPostAsync()
    {

        var user = await _userService.LoginAsync(Email, Password, LoginType);

        HttpContext.Session.SetInt32("UserID", user.Id);

        if (user.Role == "Caregiver")
        {
            return RedirectToPage("/CaregiverPage/MyTaskPage/Index");
        }

        else if (user.Role == "User")
            return RedirectToPage("./Index");

        return RedirectToPage("/Auth/Index"); // Redirect after login
    }
}
