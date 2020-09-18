using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class OsobaImage
    {
      
        [Key]
        public int OsobaImageID { get; set; }
        [ForeignKey("Osoba")]
        public int OsobaID { get; set; }
        public Osoba Osoba { get; set; }


        [Column(TypeName = "varbinary(max)")]
        public byte[] Image { get; set; }


       
    }
}
