namespace OptiFuel.Models
{
    public class Certificat
    {
        public int Id { get; set; }
        public int PlanningId { get; set; }
        public byte [] CertificatFile { get; set; }
        public DateTime DateUpload { get; set; }
        public Planning Planning { get; set; }

    }
}
