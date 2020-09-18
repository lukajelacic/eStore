using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class KupacKupuje
    {
        [ForeignKey("Kupac")]
        public int KupacID { get; set; }
        public Kupac Kupac { get; set; }

        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }
        
        [ForeignKey("Popust")]
        public int? PopustID { get; set; }
        public Popust Popust { get; set; }
        
        [Column(TypeName = "DateTime")]
        public DateTime VrijemeKupovine { get; set; }
        [Required]
        public float Cijena { get; set; } 
    }
}
