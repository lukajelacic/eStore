using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Drzava
    {
        [Key]
        public int DrzavaID { get; set; }
        [Required,MaxLength(50)]
        public string Naziv { get; set; }

        ICollection<Regija> Regije { get; set; }
    }
/*simpasdasdasde*/
}
