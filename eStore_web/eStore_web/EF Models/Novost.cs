using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Novost
    {
        [Key]
        public int NovostID { get; set; }
        [Required,MinLength(5),MaxLength(50)]
        public string Naslov { get; set; }
        [Required,MaxLength(2048)]
        public string Sadrzaj { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime DatumObjave { get; set; }

        [ForeignKey("Developer")]
        public int DeveloperID { get; set; }
        public Developer Developer { get; set; }

        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }
    }
}
