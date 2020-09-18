using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class PrijavaRecenzije
    {
        [Key]
        public int PrijavaRecenzijeID { get; set; }
        [Required,MaxLength(50)]
        public string Razlog {get; set; }

        [ForeignKey("Recenzija")]
        public int RecenzijaID { get; set; }
        public Recenzija Recenzija { get; set; }

        [ForeignKey("Kupac")]
        public int? KupacID { get; set; }
        public Kupac Kupac { get; set; }
        
        [ForeignKey("Administrator")]
        public int? AdministratorID { get; set; }
        public Administrator Administrator { get; set; }
        
    }
}
