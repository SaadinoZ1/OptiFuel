using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiFuelMaui.Models
{
    public class Commission
    {
        public Guid Id { get; set; }
        public Guid ValidationBLId { get; set; }
        public Guid ContactId { get; set; }
        public string CodeG { get; set; }
        public string CodeS { get; set; }
        public DateTime e_created_on { get; set; }
        public DateTime? e_updated_on { get; set; }


        public ValidationBL ValidationBL { get; set; }
        public Contact Contact { get; set; }
    }
}
