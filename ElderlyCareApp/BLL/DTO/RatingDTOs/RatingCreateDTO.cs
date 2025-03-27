namespace BLL.DTO.RatingDTOs
{
    public class RatingCreateDTO
    {
        public int BookingId { get; set; }
        public int CaregiverId { get; set; }
        public int UserId { get; set; }
        public int RatingScore { get; set; }
        public string Review { get; set; } = null!;
    }

}
