namespace OptiFuel.Models
{
    public class Planning
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } 
        public string? Centre { get; set; }
        public int QuantiteALivrer { get; set; }


        public ValidationBL? ValidationBL{ get; set; }
      
        public DateTime e_created_on { get; set; } = DateTime.Now;
        public DateTime? e_updated_on { get; set; }

    }
}
