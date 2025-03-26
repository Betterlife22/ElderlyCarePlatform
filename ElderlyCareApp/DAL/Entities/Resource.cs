namespace DAL.Entities
{
    public class Resource : BaseEntity
    {
        [MaxLength(50)]
        public required string Title { get; set; }
        [MaxLength(500)]
        public required string Content { get; set; }
        public string Category { get; set; } = Constants.CommonHealthConcernsInOlderAdults;
    }
}
