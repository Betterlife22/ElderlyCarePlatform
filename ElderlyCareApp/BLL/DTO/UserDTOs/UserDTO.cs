namespace BLL.DTO.UserDTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public required string Gender { get; set; } 
        public string Address { get; set; } = null!;

        // Guardian Information (Optional)
        //public string? GuardianName { get; set; }
        //public int? GuardianAge { get; set; }
        //public string? GuardianIdCard { get; set; }
    }
}
