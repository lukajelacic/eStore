using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class AdministratorPregledReportaVM
    {
        public int Id { get; set; }
        public int KupacId { get; set; }
        public string ReportovaniKupac { get; set; }
        public string RazlogReporta { get; set; }
        public int Reputacija { get; set; }
    }
}
