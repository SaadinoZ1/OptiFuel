using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiFuelMaui.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Center { get; set; }
        public int QuantiteALivrer { get; set; }
    }
}
