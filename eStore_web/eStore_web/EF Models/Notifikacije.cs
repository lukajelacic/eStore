using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Notifikacije
    {

        [Key]
        public int NotifikacijaID { get; set; }
        public DateTime Vrijeme { get; set; }
        public string Poruka { get; set; }

        public string URL { get; set; } 

        [ForeignKey("UserTo")]
        public int? UserToID { get; set; }
        public Osoba UserTo { get; set; }

        [ForeignKey("UserFrom")]
        public int? UserFromID { get; set; }
        public Osoba UserFrom { get; set; }
    }
}
