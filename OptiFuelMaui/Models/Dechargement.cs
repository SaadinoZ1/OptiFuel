using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiFuelMaui.Models
{
    public class Dechargement
    {
        public Guid Id { get; set; }
        public Guid ValidationBLId { get; set; }
        public string? Cuve { get; set; }
        public double LevelStart { get; set; }
        public double LevelEnd { get; set; }
        public double DeliveryVolume { get; set; }
        public DateTime e_created_on { get; set; }
        public DateTime? e_updated_on { get; set; }


        public ValidationBL? ValidationBL { get; set; }
    }
}
