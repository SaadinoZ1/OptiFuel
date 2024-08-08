using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiFuelMaui.Dtos
{
    public class ValidationBLDto
    {
        public Guid PlanningId { get; set; }
        public byte[] BLFile { get; set; }
        public byte[] CertificatJumelageFile { get; set; }
        public int QuantitésBL { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ObservableCollection<CommissionDto> Commissions { get; set; }
    }
}
