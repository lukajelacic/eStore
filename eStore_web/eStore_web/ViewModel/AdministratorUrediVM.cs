using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class AdministratorUrediVM
    {
        public int Id { get; set; }
        public int IgraId { get; set; }
        [Required(ErrorMessage = "Unesite naziv igre")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesite opis igre")]
        public string Opis { get; set; }
        [Required, Range(1, 100, ErrorMessage = "Unesite cijenu izmedju 1 i 100 KM")]
        public float Cijena { get; set; }
        public DateTime DatumObjave { get; set; }
        [Required, DefaultValue("false")]
        public bool Odobrena { get; set; }
        [Required, DefaultValue("false")]
        public bool PremiumStatus { get; set; }
        public List<SelectListItem> Zanrovi { get; set; }
        [Required(ErrorMessage = "Izaberite zanr igre")]
        public int ZanrId { get; set; }
        public List<SelectListItem> Kategorije { get; set; }
        [Required(ErrorMessage = "Izaberite kategoriju igre")]
        public int KategorijaId { get; set; }
    }
}
