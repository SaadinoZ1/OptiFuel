﻿namespace OptiFuel.Models
{
    public class Centre
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTime e_created_on { get; set; }
        public DateTime? e_updated_on { get; set; }
    }
}