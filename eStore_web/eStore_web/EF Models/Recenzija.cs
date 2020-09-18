using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Recenzija
    {
        [Key]
        public int RecenzijaID { get; set; }

        [ForeignKey("Kupac")]
        public int KupacID { get; set; }
        public Kupac Kupac { get; set; }

        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }

        [Required,MaxLength(1024)]
        public string RecenzijaText { get; set; }
        [Required,Range(1,10)]
        public int Ocjena { get; set; }


        public ICollection<PrijavaRecenzije>PrijavaRecenzija { get; set; }
    }
}
