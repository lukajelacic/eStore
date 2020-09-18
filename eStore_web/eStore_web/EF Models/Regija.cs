using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Regija
    {
        [Key]
        public int RegijaID { get; set; }
        [Required, MaxLength(50)]
        public string Naziv { get; set; }


        [ForeignKey("Drzava")]
        public int DrzavaID { get; set; }
        public Drzava Drzava { get; set; }

        public ICollection<Grad> Gradovi { get; set; }
    }
}
