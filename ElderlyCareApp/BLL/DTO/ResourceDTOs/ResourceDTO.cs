﻿namespace BLL.DTO.ResourceDTOs
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; } 
    }
}
