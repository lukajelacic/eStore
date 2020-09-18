using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Report
    {
            public int Id { get; set; }
            public string RazlogReporta { get; set; }
            public Kupac ReportovaniKupac { get; set; }
            public int ReportovaniKupacId { get; set; }
    }
}
