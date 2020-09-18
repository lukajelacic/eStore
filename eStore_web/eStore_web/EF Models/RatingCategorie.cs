using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStore_web.EF_Models
{
    public class RatingCategorie
    {
        [Key]
        public int RatingCategorieID { get; set; }
        [Required, MaxLength(50)]
        public string NazivKategorije { get; set; }
        [Required, MaxLength(10)]
        public string OznakaKategorije { get; set; }

        public ICollection<Igra> Igre { get; set; }
    }
}
