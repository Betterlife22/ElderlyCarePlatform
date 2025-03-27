namespace BLL.DTO.UserDTOs
{
    public class UserCreateDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public DateTime BirthDate { get; set; }
        public required string Gender { get; set; } 
        public required string Address { get; set; }

        // Guardian Information (Optional)
        public string? GuardianName { get; set; }
        public int? GuardianAge { get; set; }
        public string? GuardianIdCard { get; set; }
    }
}
