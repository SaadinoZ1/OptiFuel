namespace OptiFuel.Models
{
    public class ValidationBL
    {
        public Guid Id { get; set; }
        public Guid PlanningId { get; set; }
        public byte [] BLFile { get; set; }
        public byte[] CertificatJumelageFile { get; set; }

        public int QuantitésBL { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime e_created_on { get; set; }
        public DateTime? e_updated_on { get; set; }

        public Planning Planning { get; set; }
        public ICollection<Dechargement> Dechargements { get; set; }
        public ICollection<Commission> Commissions { get; set; }
        
    }
}
