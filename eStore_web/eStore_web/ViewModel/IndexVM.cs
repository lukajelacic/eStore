using System;
using System.Collections.Generic;
using System.Linq;
using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStore_web.Common;
using eStore_web.ViewModel;

namespace eStore_web.ViewModel
{
    public class KupacIndexVM
    {

        public string pretraga { get; set; }
        public int sortirajPo { get; set; }
        public int zanr { get; set; }
        public int kategorija { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public List<int> PopustProcent { get; set; }
        public List<double> novaCijena { get; set; }
        public List<RatingCategorie> ratingCategorie { get; set; }
        public List<GameGenre> gameGenre { get; set; }
        public List<Igra> Igre { get; set; }

        public KupacIndexVM()
        {
            pretraga = null;
            sortirajPo = 0;
            zanr = 0;
            kategorija = 0;
            from = 0;
            to = 20;
            PopustProcent = new List<int>();
            novaCijena = new List<double>();
        }
    }
}
