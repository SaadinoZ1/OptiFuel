namespace OptiFuel.Models
{
    public class Commission
    {
        public Guid Id { get; set; }
        public Guid ValidationBLId { get; set; }
        public Guid ContactId { get; set; }
        public string? CodeG { get; set; }
        public string? CodeS { get; set; }
        public DateTime e_created_on { get; set; } 
        public DateTime? e_updated_on { get; set; }


        public ValidationBL? ValidationBL { get; set; }
        public Contact? Contact { get; set; }


    }
}
