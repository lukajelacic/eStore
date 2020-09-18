using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class PrijavaPremium
    {
        public int Id { get; set; }

        public Kupac Kupac { get; set; }
        public int KupacId { get; set; }

        public DateTime DatumPrijave { get; set; }
    }
}
