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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userService.LoginAsync(Email, Password, LoginType);

        HttpContext.Session.SetInt32("UserID", user.Id);

        if (user.Role == "Staff")
        {
            return RedirectToPage("/Staff/Index");
        }

        else if (user.Role == "User")
            return RedirectToPage("/Auth/Login");

        return RedirectToPage("/Auth/Index"); // Redirect after login
    }
}
