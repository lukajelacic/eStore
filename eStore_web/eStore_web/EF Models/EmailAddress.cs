using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class EmailAddress
    {
        [Key]
        public int EmailAddressID { get; set; }
        [Required,MaxLength(30),EmailAddress]
        public string Email { get; set; }


        [ForeignKey("Osoba")]
        public int? OsobaID { get; set; }
        public Osoba Osoba { get; set; }
    }
}
