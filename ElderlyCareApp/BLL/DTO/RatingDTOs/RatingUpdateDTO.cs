namespace BLL.DTO.RatingDTOs
{
    public class RatingUpdateDTO
    {
        public int RatingScore { get; set; }
        public string Review { get; set; } = null!;
    }

}
