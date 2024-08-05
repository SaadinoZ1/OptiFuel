using System.Collections.ObjectModel;

namespace OptiFuel.Dtos
{
    public class ValidationBLDto
    {
        public Guid PlanningId { get; set; }
        public byte[] BLFile { get; set; }
        public byte[] CertificatJumelageFile { get; set; }
        public int QuantitésBL { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ObservableCollection<CommissonDto> Commissions { get; set; }
    }
}
