using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class AdministratorDetaljiVM
    {
        public int DeveloperID { get; set; }
        public string ImePrezime { get; set; }
        public string Grad { get; set; }
        public string Kompanija { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
    }
}
