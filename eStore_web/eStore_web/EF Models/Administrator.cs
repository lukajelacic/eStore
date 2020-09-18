using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class Administrator
    {

        [Key]
        public int AdministratorID { get; set; }


        
        [ForeignKey("Osoba")]
        public int OsobaID { get; set; }
        public Osoba Osoba { get; set; }
        
    }
}