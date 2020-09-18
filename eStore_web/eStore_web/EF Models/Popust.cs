using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Popust
    {
        [Key]
        public int PopustID { get; set; }

        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime PocetakPopusta { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime KrajPopusta { get; set; }

        [Required,Range(1,100)]
        public int PopustProcent { get; set; }


        public ICollection<KupacKupuje> KupacKupuje { get; set; }
    }
}
