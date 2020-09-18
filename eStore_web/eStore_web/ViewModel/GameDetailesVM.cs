using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore_web.EF_Models;
namespace eStore_web.ViewModel
{
    public class GameDetailesVM
    {
       
        public Popust Popust { get; set; }
        public double NovaCijena { get; set; }
        public Igra Igra { get; set; }
        public List<Recenzija> Recenzije { get; set; }
        public double Ocjena { get; set; }
        public bool ImaRecenziju { get; set; }
        public bool ImaWishList { get; set; }
        public bool ImaIgru { get; set; }
        public bool PratiIgru { get; set; }
        public string GameTrailer { get; set; }
    } 
}
