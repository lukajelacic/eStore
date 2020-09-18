using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Grad
    {
        [Key]
        public int GradID { get; set; }
        [Required, MaxLength(50)]
        public string Naziv { get; set; }


        [ForeignKey("Regija")]
        public int RegijaID { get; set; }
        public Regija Regija { get; set; }

        public ICollection<Osoba> Osoba { get; set; }
    }
}
