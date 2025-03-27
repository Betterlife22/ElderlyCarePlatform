namespace BLL.DTO.ServiceDTOs
{
    public class ServiceUpdateDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
