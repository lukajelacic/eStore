using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class IgricaImage
    {
        [Key]
        public int IgricaImageID { get; set; }
        [ForeignKey("Igra")]
        public int IgraID { get; set; }
        [Column(TypeName = "varbinary(max)")]
        public byte[] Image { get; set; }
    }
}
