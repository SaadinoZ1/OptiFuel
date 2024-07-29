namespace OptiFuel.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Télephone { get; set; }
        public string Email { get; set; }
        public string Poste { get; set; }
        public DateTime e_created_on { get; set; }
        public DateTime? e_updated_on { get; set; }
    }
}
