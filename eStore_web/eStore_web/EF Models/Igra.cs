using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStore_web.EF_Models
{
    public class Igra
    {
        [Key]
        public int IgraID { get; set; }
        [Required,MaxLength(50)]
        public string Naziv { get; set; }
        [Required,MaxLength(1048)]
        public string Opis { get; set; }
        [Required,Range(1,999)]
        public float Cijena { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DatumObjave { get; set; }
        [Required, MaxLength(1048)]
        public string TrailerUrl { get; set; }
        [Required,DefaultValue("false")]
        public bool PremiumStatus { get; set; }
        [Required,DefaultValue("false")]
        public bool Odobrena { get; set; }

        
        [ForeignKey("GameGenre")]
        public int? GameGenreID { get; set; }
        public GameGenre GameGenre { get; set; }

        [ForeignKey("RatingCategorie")]
        public int? RatingCategorieID { get; set; }
        public RatingCategorie RatingCategorie { get; set; }
        
        [ForeignKey("Developer")]
        public int? DeveloperID { get; set; }
        public Developer Developer { get; set; }

        [ForeignKey("Administrator")]
        public int? AdministratorID { get; set; }
        public Administrator Administrator { get; set; }
        
        [ForeignKey("IgricaImage")]
        public int? IgricaImageID { get; set; }
        public IgricaImage IgricaImage { get; set; }


        public ICollection<Novost> Novost { get; set; }
        public ICollection<PreuzimanjeIgre> PreuzimanjeIgre { get; set; }
        public ICollection<KupacKupuje> KupacKupuje { get; set; }
        public ICollection<Recenzija> Recenzija { get; set; }

    }
}
