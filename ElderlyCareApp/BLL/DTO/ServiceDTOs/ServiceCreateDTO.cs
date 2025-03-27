namespace BLL.DTO.ServiceDTOs
{
    public class ServiceCreateDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
