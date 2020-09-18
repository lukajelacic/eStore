using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Developer
    {
       [Key]
       public int DeveloperID { get; set; }

        [MaxLength(50)]
        public string NazivKompanije { get; set; }

        [ForeignKey("Osoba")]
        public int OsobaID { get; set; }
        public Osoba Osoba { get; set; }
        public bool Banovan { get; set; }

        public ICollection<Novost> Novosti { get; set; }
    }
}
