﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Prati
    {
        [Key]
        public int PratiID { get; set; }

        [ForeignKey("Kupac")]
        public int KupacID { get; set; }
        public Kupac Kupac { get; set; }

        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }

        
    }
}
