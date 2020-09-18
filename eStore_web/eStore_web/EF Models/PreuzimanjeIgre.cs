using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class PreuzimanjeIgre
    {
        [Key]
        public int PreuzimanjeIgreID { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime VrijemePreuzimanja { get; set; }

        [ForeignKey("Kupac")]
        public int KupacID { get; set; }
        public Kupac Kupac { get; set; }

        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }
    }
}
