﻿namespace BLL.DTO.ProfileDTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string MedicalHistory { get; set; } = default!;
        public string Preferences { get; set; } = default!;
        public string GuardianName { get; set; } = default!;
        public int GuardianAge { get; set; }
        public string GuardianIdCard { get; set; } = default!;
        public string HealthConditions { get; set; } = default!;
    }
}
