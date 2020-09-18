using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class LoginInfo
    {
        [Key]
        public int LoginInfoID { get; set; }
        [Required,MaxLength(30)]
        public string AccountName { get; set; }
        [Required,MaxLength(30)]
        public string Password { get; set; }
        [Range(1,3)]
        public int TipKorisnika { get; set; }

        [ForeignKey("Osoba")]
        public int OsobaID { get; set; }
        public Osoba Osoba { get; set; }

        public string SignalRToken { get; set; }
    }
}
