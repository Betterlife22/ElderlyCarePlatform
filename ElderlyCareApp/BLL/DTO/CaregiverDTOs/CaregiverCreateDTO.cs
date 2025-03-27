namespace BLL.DTO.CaregiverDTOs
{
    public class CaregiverCreateDTO
    {
        public int UserId { get; set; }
        public string Experience { get; set; } = default!;
        public string Certifications { get; set; } = default!;
        public string Specialization { get; set; } = default!;
    }
}
