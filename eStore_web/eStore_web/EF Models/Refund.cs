using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Refund
    {
        [Key]
        public int RefundID { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime VrijemeKupovine { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime VrijemeZahtijeva { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime VrijemeOdgovora { get; set; }
        [MaxLength(50)]
        public string RazlogRefunda { get; set; }



        [ForeignKey("Igra")]
        public int? IgraID { get; set; }
        public Igra Igra { get; set; }
        [ForeignKey("Kupac")]
        public int? KupacID { get; set; }
        public Kupac Kupac { get; set; }

        [ForeignKey("Administrator")]
        public int? AdministratorID { get; set; }
        public Administrator Administrator { get; set; }
        
    }
}
