using System.ComponentModel.DataAnnotations;

namespace BLL.DTO.UserDTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public required string Role { get; set; }

        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public required string Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public required string Address { get; set; }

        // Guardian Information (Optional)
        public string? GuardianName { get; set; }
        public int? GuardianAge { get; set; }
        public string? GuardianIdCard { get; set; }
    }
}
