using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class LoginModel : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string LoginType { get; set; } = "User";

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }



        return RedirectToPage("/Index"); // Redirect after login
    }
}
