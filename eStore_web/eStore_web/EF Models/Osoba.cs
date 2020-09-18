using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Osoba
    {
        [Key]
        public int OsobaID { get; set; }
        [MaxLength(50)]
        public string Ime { get; set; }
        [MaxLength(50)]
        public string Prezime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DatumRodenja { get; set; }

        
        [ForeignKey("LoginInfo")]
        public int? LoginInfoID { get; set; }
        public LoginInfo LoginInfo { get; set; }

        [ForeignKey("EmailAddress")]
        public int? EmailAddressID { get; set; }
        public EmailAddress EmailAddress { get; set; }
        [ForeignKey("OsobaImage")]
        public int? OsobaImageID { get; set; }
        public OsobaImage OsobaImage { get; set; }
        [ForeignKey("Grad")]
        public int? GradID { get; set; }
        public Grad Grad { get; set; }

    }
}
