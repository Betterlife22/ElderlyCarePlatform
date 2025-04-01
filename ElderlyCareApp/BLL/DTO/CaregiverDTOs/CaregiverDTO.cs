namespace BLL.DTO.CaregiverDTOs
{
    public class CaregiverDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Experience { get; set; } = default!;
        public string Certifications { get; set; } = default!;
        public string Specialization { get; set; } = default!;
        public float Rating { get; set; }
    }
}
