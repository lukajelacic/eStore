using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStore_web.EF_Models
{
    public class GameGenre
    {
        [Key]
        public int GameGenreID { get; set; }
        [Required,MaxLength(50)]
        public string NazivZanra { get; set; }
        [Required,MaxLength(10)]
        public string OznakaZanra { get; set; }

        public ICollection<Igra> Igra { get; set; }
    }
}
